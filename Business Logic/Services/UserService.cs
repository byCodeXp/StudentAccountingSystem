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
using Business_Logic.Utils;
using Data_Transfer_Objects.EmailTemplates;
using Data_Transfer_Objects.Requests;
using Data_Transfer_Objects.ViewModels;
using SendGrid.Helpers.Mail;

namespace Business_Logic.Services
{
    public class UserService
    {
        private readonly IJwtUtility jwtUtility;
        private readonly UserManager<User> userManager;
        private readonly CourseQuery courseQuery;
        private readonly UserCommand userCommand;
        private readonly UserQuery userQuery;
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly EmailService emailService;
        private readonly RazorTemplateHelper razorTemplateHelper;

        public UserService(
            IJwtUtility jwtUtility,
            UserManager<User> userManager,
            IMapper mapper,
            DataContext context, EmailService emailService, RazorTemplateHelper razorTemplateHelper)
        {
            this.jwtUtility = jwtUtility;
            this.userManager = userManager;
            this.mapper = mapper;
            this.context = context;
            this.emailService = emailService;
            this.razorTemplateHelper = razorTemplateHelper;
            userCommand = new(context);
            courseQuery = new(context);
            userQuery = new(context);
        }

        private int Offset (int page, int perPage) => page <= 1 ? 0 : page * perPage - perPage;
        
        public UserVM GetUsers(UsersRequest request)
        {
            // TODO: add sort

            var users = userQuery.GetAll().OrderBy(m => m.CreatedTimeStamp).Skip(Offset(request.Page, request.PerPage)).Take(request.PerPage);
            
            return new UserVM
            {
                TotalCount = userQuery.GetCount(),
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
            
            var userId = jwtUtility.DecodeToken(token).Issuer;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundRestException($"User was not found");
            }

            var courses = courseQuery.GetCoursesByUserId(user.Id);

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

            var userId = jwtUtility.DecodeToken(token).Issuer;

            var user = await userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                throw new NotFoundRestException($"User was not found");
            }

            if (userQuery.GetCoursesByUser(user).Any(m => m.Id == course.Id))
            {
                throw new BadRequestRestException($"Course with id: {request.CourseId}, cannot be subscribed on user with id: {user.Id}");
            }
            
            userCommand.SubscribeCourse(user, course);
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
            // TODO: remove jobs from hangfire
            
            var course = courseQuery.GetOne(courseId);

            if (course == null)
            {
                throw new NotFoundRestException($"Course with id: {courseId}, was not found");
            }
            
            var editedToken = token.Replace("Bearer ", "");
            
            var userId = jwtUtility.DecodeToken(editedToken).Issuer;

            var user = await userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                throw new NotFoundRestException($"User was not found");
            }

            if (userQuery.GetCoursesByUser(user).All(m => m.Id != courseId))
            {
                throw new BadRequestRestException($"Course with id: {courseId}, cannot be unsubscribed on user with id: {user.Id}");
            }
            
            userCommand.UnsubscribeCourse(user, course);
            await context.SaveChangesAsync();
        }
    }
}
