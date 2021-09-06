using System;
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
using Data_Transfer_Objects.Requests;
using Data_Transfer_Objects.ViewModels;

namespace Business_Logic.Services
{
    public class UserService
    {
        private readonly UserManager<User> userManager;
        private readonly CourseQuery courseQuery;
        private readonly UserCommand userCommand;
        private readonly UserQuery userQuery;
        private readonly JwtService jwtService;
        private readonly IMapper mapper;

        public UserService(
            UserManager<User> userManager,
            JwtService jwtService,
            IMapper mapper,
            DataContext context)
        {
            this.userManager = userManager;
            this.jwtService = jwtService;
            this.mapper = mapper;
            userCommand = new(context);
            courseQuery = new(context);
            userQuery = new(context);
        }

        private int Offset (int page, int perPage) => page <= 1 ? 0 : page * perPage - perPage;
        
        public UserVM GetUsers(GetUsersRequest request)
        {
            var users = userQuery.GetAll();

            users = users.OrderBy(m => m.CreatedTimeStamp).Skip(Offset(request.Page, request.PerPage)).Take(request.PerPage);

            var userVM = new UserVM
            {
                TotalCount = userQuery.GetCount(),
                Users = mapper.Map<UserDTO[]>(users)
            };
            
            // TODO: also return users role
            
            return userVM;
        }
        
        public UserDTO GetUserById(Guid id)
        {
            var user = userQuery.GetOne(id);

            if (user == null)
            {
                throw new HttpResponseException($"User with id: {id}, was not found");
            }

            return mapper.Map<UserDTO>(user);
        }

        public async Task SubscribeUserOnCourseAsync(Guid courseId, string token)
        {
            var course = courseQuery.GetOne(courseId);

            if (course == null)
            {
                throw new HttpResponseException($"Course with id: {courseId}, was not found");
            }

            var userId = jwtService.Verify(token).Issuer;

            var user = await userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                throw new HttpResponseException($"User was not found");
            }

            var result = userCommand.SubscribeCourse(user, course);

            if (!result)
            {
                throw new HttpResponseException($"Course with id: {courseId}, cannot be subscribed on user with id: {user.Id}");
            }
        }
    }
}
