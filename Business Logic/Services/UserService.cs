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
        private readonly UserManager<User> _userManager;
        private readonly CourseQuery _courseQuery;
        private readonly UserCommand _userCommand;
        private readonly UserQuery _userQuery;
        private readonly JwtService _jwtService;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<User> userManager,
            JwtService jwtService,
            IMapper mapper,
            DataContext context)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _userCommand = new (context);
            _courseQuery = new(context);
            _userQuery = new(context);
        }

        public UserVM GetUsers(GetPageRequest request)
        {
            var users = _userQuery.GetAll();

            switch (request.SortBy)
            {
                case SortBy.Asc:
                    users = users.OrderBy(m => m.CreatedTimeStamp);
                    break;
                case SortBy.Desc:
                    users = users.OrderByDescending(m => m.CreatedTimeStamp);
                    break;
            }

            users = users.Skip(request.Offset).Take(request.PerPage);

            var userVM = new UserVM
            {
                TotalCount = _userQuery.GetCount(),
                Users = _mapper.Map<UserDTO[]>(users)
            };
            
            // TODO: also return users role
            
            return userVM;
        }
        
        public UserDTO GetUserById(Guid id)
        {
            var user = _userQuery.GetOne(id);

            if (user == null)
            {
                throw new HttpResponseException($"User with id: {id}, was not found");
            }

            return _mapper.Map<UserDTO>(user);
        }

        public async Task SubscribeUserOnCourseAsync(Guid courseId, string token)
        {
            var course = _courseQuery.GetOne(courseId);

            if (course == null)
            {
                throw new HttpResponseException($"Course with id: {courseId}, was not found");
            }

            var userId = _jwtService.Verify(token).Issuer;

            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                throw new HttpResponseException($"User was not found");
            }

            var result = _userCommand.SubscribeCourse(user, course);

            if (!result)
            {
                throw new HttpResponseException($"Course with id: {courseId}, cannot be subscribed on user with id: {user.Id}");
            }
        }
    }
}
