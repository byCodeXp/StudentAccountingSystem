using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Queries;
using Data_Transfer_Objects.Entities;
using Business_Logic.Exceptions;
using Business_Logic.Helpers;
using Data_Transfer_Objects.EmailTemplates;
using Data_Transfer_Objects.Requests;
using Data_Transfer_Objects.ViewModels;
using Hangfire;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace Business_Logic.Services
{
    public class UserService
    {
        private readonly UserManager<User> userManager;
        private readonly CourseQuery courseQuery;
        private readonly UserQuery userQuery;
        private readonly SubscribeCommand subscribeCommand;
        private readonly SubscribeQuery subscribeQuery;
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly EmailService emailService;
        private readonly RazorTemplateHelper razorTemplateHelper;
        private readonly JobService jobService;
        private readonly IJwtHelper jwtHelper;
        private readonly ILogger<UserService> logger;

        public UserService(
            UserManager<User> userManager,
            IMapper mapper,
            DataContext context, EmailService emailService, RazorTemplateHelper razorTemplateHelper, JobService jobService, ILogger<UserService> logger, IJwtHelper jwtHelper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.context = context;
            this.emailService = emailService;
            this.razorTemplateHelper = razorTemplateHelper;
            this.jobService = jobService;
            this.logger = logger;
            this.jwtHelper = jwtHelper;
            subscribeCommand = new(context);
            subscribeQuery = new(context);
            courseQuery = new(context);
            userQuery = new(context);
        }

        private int Offset (int page, int perPage) => page <= 1 ? 0 : page * perPage - perPage;
        
        public UserVM GetUsers(UsersRequest request)
        {
            var users = userQuery.GetAll();

            // Search
            
            if (string.IsNullOrEmpty(request.SearchByFirstName))
            {
                users = users.Where(m => m.FirstName.Contains(request.SearchByFirstName) || m.LastName.Contains(request.SearchByFirstName));
            }
            
            if (string.IsNullOrEmpty(request.SearchByLastName))
            {
                users = users.Where(m => m.FirstName.Contains(request.SearchByLastName) || m.LastName.Contains(request.SearchByLastName));
            }
            
            // Sort
            
            switch (request.SortBy)
            {
                case "FirstName":
                    users = request.Ascending ? users.OrderBy(m => m.FirstName) : users.OrderByDescending(m => m.FirstName);
                    break;
                case "LastName":
                    users = request.Ascending ? users.OrderBy(m => m.LastName) : users.OrderByDescending(m => m.LastName);
                    break;
                case "Age":
                    users = request.Ascending ? users.OrderBy(m => m.Age) : users.OrderByDescending(m => m.Age);
                    break;
            }

            // Total count
            
            var count = users.Count();
            
            // Pagination
            
            users = users.Skip(Offset(request.Page, request.PerPage)).Take(request.PerPage);
            
            return new UserVM
            {
                TotalCount = count,
                Users = mapper.Map<List<UserDTO>>(users)
            };
        }
        
        public UserDTO GetUserById(Guid id)
        {
            var user = userQuery.GetById(id);

            if (user == null)
            {
                throw new NotFoundRestException($"User with id: {id}, was not found");
            }

            return mapper.Map<UserDTO>(user);
        }

        public async Task<List<CourseDTO>> GetUserCourses(string bearerToken)
        {
            var token = bearerToken.Split(" ").Last();
            
            var userId = jwtHelper.DecodeToken(token).Issuer;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundRestException($"User was not found");
            }

            var courses = subscribeQuery.GetUserCourses(user);

            return mapper.Map<List<CourseDTO>>(courses);
        }

        public async Task SubscribeCourseAsync(SubscribeRequest request, string bearerToken)
        {
            var token = bearerToken.Split(" ").Last();
            
            var course = courseQuery.GetOne(request.CourseId);

            if (course == null)
            {
                throw new NotFoundRestException($"Course with id: {request.CourseId}, was not found");
            }

            var userId = jwtHelper.DecodeToken(token).Issuer;

            var user = await userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                throw new NotFoundRestException($"User was not found");
            }

            if (userQuery.GetCoursesByUser(user).Any(m => m.Id == course.Id))
            {
                throw new BadRequestRestException($"Course with id: {request.CourseId}, cannot be subscribed on user with id: {user.Id}");
            }

            var jobs = jobService.ScheduleCourseReminder(mapper.Map<UserDTO>(user), mapper.Map<CourseDTO>(course), request.Date);

            subscribeCommand.Subscribe(user, course, jobs.ToList());
            await context.SaveChangesAsync();
            
            var template = await razorTemplateHelper.GetTemplateHtmlAsStringAsync("Subscribed", new SubscribedEmailModel()
            {
                Preview = course.Preview,
                Title = course.Name,
                Date = request.Date.ToShortDateString()
            });
            
            await emailService.SendMailAsync("Course subscribe success", new EmailAddress(user.Email), template);
            
        }

        public async Task UnsubscribeFromCourse(Guid courseId, string token)
        {
            var course = courseQuery.GetOne(courseId);

            if (course == null)
            {
                throw new NotFoundRestException($"Course with id: {courseId}, was not found");
            }
            
            var editedToken = token.Replace("Bearer ", "");
            
            var userId = jwtHelper.DecodeToken(editedToken).Issuer;

            var user = await userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                throw new NotFoundRestException($"User was not found");
            }

            var subscribe = subscribeCommand.Unsubscribe(user, course);
            
            if (subscribe == null)
            {
                throw new BadRequestRestException($"Course with id: {courseId}, cannot be unsubscribed on user with id: {user.Id}");
            }

            foreach (var job in subscribe.Jobs)
            {
                BackgroundJob.Delete(job);
            }
            
            await context.SaveChangesAsync();
        }
    }
}
