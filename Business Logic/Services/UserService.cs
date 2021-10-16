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
        private readonly UserCommand userCommand;
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly EmailService emailService;
        private readonly RazorTemplateHelper razorTemplateHelper;
        private readonly JobService jobService;
        private readonly ILogger<UserService> logger;

        public UserService(
            UserManager<User> userManager,
            IMapper mapper,
            DataContext context, EmailService emailService, RazorTemplateHelper razorTemplateHelper, JobService jobService, ILogger<UserService> logger)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.context = context;
            this.emailService = emailService;
            this.razorTemplateHelper = razorTemplateHelper;
            this.jobService = jobService;
            this.logger = logger;
            courseQuery = new(context);
            userQuery = new(context);
            userCommand = new(context);
        }

        private int Offset (int page, int perPage) => page <= 1 ? 0 : page * perPage - perPage;
        
        public UserVM GetUsers(UsersRequest request)
        {
            var users = userQuery.GetAll();
            
            // Search
            
            if (!string.IsNullOrEmpty(request.Search))
            {
                users = userQuery.SearchByName(request.Search);
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
                case "BirthDay":
                    users = request.Ascending ? users.OrderBy(m => m.BirthDay) : users.OrderByDescending(m => m.BirthDay);
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
        
        public UserDTO GetUserById(string id)
        {
            var user = userQuery.GetById(id);

            if (user == null)
            {
                throw new NotFoundRestException($"User with id: {id}, was not found");
            }

            return mapper.Map<UserDTO>(user);
        }

        public async Task<List<CourseDTO>> GetUserCourses(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundRestException($"User was not found");
            }

            var courses = userQuery.UserCourses(user);

            return mapper.Map<List<CourseDTO>>(courses);
        }

        public async Task SubscribeCourseAsync(SubscribeRequest request, string userId)
        {
            var course = courseQuery.GetOne(request.CourseId);

            if (course == null)
            {
                throw new NotFoundRestException($"Course with id: {request.CourseId}, was not found");
            }

            var user = userQuery.GetById(userId);
            
            if (user == null)
            {
                throw new NotFoundRestException($"User was not found");
            }

            if (userQuery.UserExistsCourse(user, course))
            {
                throw new BadRequestRestException($"Course with id: {request.CourseId}, cannot be subscribed on user with id: {user.Id}");
            }

            var jobs = await jobService.ScheduleCourseReminder(mapper.Map<UserDTO>(user), mapper.Map<CourseDTO>(course), request.Date);

            userCommand.SubscribeCourse(user, course, jobs.ToList());
            
            await context.SaveChangesAsync();
            
            var template = await razorTemplateHelper.GetTemplateHtmlAsStringAsync("Subscribed", new SubscribedEmailModel()
            {
                Preview = course.Preview,
                Title = course.Name,
                Date = request.Date.ToShortDateString()
            });
            
            await emailService.SendMailAsync("Course subscribe success", new EmailAddress(user.Email), template);
        }

        public async Task UnsubscribeFromCourse(Guid courseId, string userId)
        {
            var course = courseQuery.GetOne(courseId);

            if (course == null)
            {
                throw new NotFoundRestException($"Course with id: {courseId}, was not found");
            }
            
            var user = await userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                throw new NotFoundRestException($"User was not found");
            }

            var jobs = userCommand.UnsubscribeCourse(user, course);
            
            if (jobs == null)
            {
                throw new BadRequestRestException($"Course with id: {courseId}, cannot be unsubscribed on user with id: {user.Id}");
            }

            foreach (var job in jobs)
            {
                BackgroundJob.Delete(job);
            }
            
            await context.SaveChangesAsync();
        }
    }
}
