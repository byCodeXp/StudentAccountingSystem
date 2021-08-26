using System;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Models;
using Data_Access_Layer.Queries;
using Business_Logic.Errors;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data_Transfer_Objects.Entities;
using Data_Transfer_Objects.Requests;
using Data_Transfer_Objects.ViewModels;
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

        public CourseVM GetCourses(GetPageRequest request)
        {
            var courses = _courseQuery.GetAll();

            switch (request.SortBy)
            {
                case SortBy.Asc:
                    courses = courses.OrderBy(m => m.Name);
                    break;
                case SortBy.Desc:
                    courses = courses.OrderByDescending(m => m.Name);
                    break;
            }
            
            courses = courses.Skip(request.Offset).Take(request.PerPage);
            
            var courseVm = new CourseVM
            {
                TotalCount = _courseQuery.GetCount(),
                Courses = _mapper.Map<CourseDTO[]>(courses)
            };

            return courseVm;
        }

        public CourseDTO GetCourseById(Guid id)
        {
            Course course = _courseQuery.GetOne(id);

            if (course == null)
            {
                throw new HttpResponseException($"Course with id: {id}, was not found");
            }

            return _mapper.Map<CourseDTO>(course);
        }

        public CourseDTO CreateCourse(CourseDTO course)
        {
            var result = _courseCommand.Create(_mapper.Map<Course>(course));

            if (!result)
            {
                throw new HttpResponseException($"Course with name {course.Name}, cannot be created");
            }
            
            return course;
        }

        public void DeleteCourse(Guid id)
        {
            if (!_courseCommand.Delete(id))
            {
                throw new HttpResponseException($"Course with id: {id}, cannot be deleted");
            }
        }

        public CourseDTO EditCourse(Guid id, CourseDTO course)
        {
            var result = _courseCommand.Update(id, _mapper.Map<Course>(course));

            if (!result)
            {
                throw new HttpResponseException($"Course with id: {id}, cannot be updated");
            }

            return course;
        }
        
        public async Task SubscribeUser(string token, Guid courseId)
        {
            var userId = _jwtService.Verify(token).Issuer;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new HttpResponseException($"User was not found");
            }

            var course = _courseQuery.GetOne(courseId);

            if (course == null)
            {
                throw new HttpResponseException($"Course with id: {courseId}, was not found");
            }

            if (!_userCommand.SubscribeCourse(user, course))
            {
                throw new HttpResponseException($"Current user cannot be subscribed, on course with id: {courseId}");
            }
        }
    }
}