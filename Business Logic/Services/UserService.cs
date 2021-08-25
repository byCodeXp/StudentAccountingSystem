using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity;
using Data_Transfer_Objects;
using AutoMapper;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Queries;
using Data_Transfer_Objects.Errors;

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

        public IEnumerable<UserDTO> GetUsers(int page, int perPage)
        {
            int offset = page <= 1 ? 0 : page * perPage - perPage;

            var users = _userQuery.GetAll().Skip(offset).Take(perPage); // TODO: order by

            // TODO: also return users role
            
            return _mapper.Map<IEnumerable<UserDTO>>(users);
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
