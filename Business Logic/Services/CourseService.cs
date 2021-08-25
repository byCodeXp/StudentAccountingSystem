using System;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Models;
using Data_Access_Layer.Queries;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Errors;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Business_Logic.Services
{
    public class CourseService
    {
        private readonly JwtService _jwtService;
        private readonly CourseQuery _courseQuery;
        private readonly CourseCommand _courseCommand;
        private readonly UserCommand _userCommand;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CourseService(DataContext context, IMapper mapper, JwtService jwtService, UserManager<User> userManager)
        {
            _userCommand = new(context);
            _courseQuery = new(context);
            _courseCommand = new(context);
            _mapper = mapper;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public IEnumerable<CourseDTO> GetCourses(int page, int perPage)
        {
            int offset = page <= 1 ? 0 : page * perPage - perPage;

            var courses = _courseQuery.GetAll().OrderBy(m => m.CreatedTimeStamp).Skip(offset).Take(perPage);

            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        public CourseDTO GetCourseById(Guid id)
        {
            Course course = _courseQuery.GetOne(id);

            if (course == null)
            {
                throw new HttpResponseException($"Course with id: {id}, was not found") { HttpStatusCode = HttpStatusCode.NotFound };
            }

            return _mapper.Map<CourseDTO>(course);
        }

        public CourseDTO CreateCourse(CourseDTO course)
        {
            var result = _courseCommand.Create(_mapper.Map<Course>(course));

            if (!result)
            {
                throw new HttpResponseException($"Course with name {course.Name}, cannot be created") { HttpStatusCode = HttpStatusCode.BadRequest };
            }
            
            return course;
        }

        public void DeleteCourse(Guid id)
        {
            if (!_courseCommand.Delete(id))
            {
                throw new HttpResponseException($"Course with id: {id}, cannot be deleted") { HttpStatusCode = HttpStatusCode.BadRequest };
            }
        }

        public CourseDTO EditCourse(Guid id, CourseDTO course)
        {
            var result = _courseCommand.Update(id, _mapper.Map<Course>(course));

            if (!result)
            {
                throw new HttpResponseException($"Course with id: {id}, cannot be updated") { HttpStatusCode = HttpStatusCode.BadRequest };
            }

            return course;
        }
        
        public async Task SubscribeUser(string token, Guid courseId)
        {
            var userId = _jwtService.Verify(token).Issuer;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new HttpResponseException($"User was not found") { HttpStatusCode = HttpStatusCode.BadRequest };
            }

            var course = _courseQuery.GetOne(courseId);

            if (course == null)
            {
                throw new HttpResponseException($"Course with id: {courseId}, was not found") { HttpStatusCode = HttpStatusCode.NotFound };
            }

            if (!_userCommand.SubscribeCourse(user, course))
            {
                throw new HttpResponseException($"Current user cannot be subscribed, on course with id: {courseId}") { HttpStatusCode = HttpStatusCode.BadRequest };
            }
        }
    }
}