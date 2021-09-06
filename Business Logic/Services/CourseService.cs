using System;
using System.Collections.Generic;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Models;
using Data_Access_Layer.Queries;
using Business_Logic.Exceptions;
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
        private readonly JobService jobService;
        private readonly JwtService jwtService;
        private readonly CourseQuery courseQuery;
        private readonly CourseCommand courseCommand;
        private readonly CategoryQuery categoryQuery;
        private readonly UserCommand userCommand;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public CourseService(DataContext context, IMapper mapper, JwtService jwtService, UserManager<User> userManager, JobService jobService)
        {
            userCommand = new(context);
            categoryQuery = new(context);
            courseQuery = new(context);
            courseCommand = new(context);
            this.mapper = mapper;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.jobService = jobService;
        }

        private int Offset (int page, int perPage) => page <= 1 ? 0 : page * perPage - perPage;
        
        public CourseVM GetCourses(GetCoursesRequest request)
        {
            var courses = courseQuery.GetAll();

            if (request.Categories != null)
            {
                var categories = request.Categories.Select(m => categoryQuery.GetByName(m));

                var filterCourses = new List<Course>();
                
                foreach (var category in categories)
                {
                    foreach (var course in courses.Where(m => m.Categories.Contains(category)))
                    {
                        if (!filterCourses.Contains(course))
                        {
                            filterCourses.Add(course);
                        }
                    }
                }

                courses = filterCourses.AsQueryable();
            }
            
            switch (request.SortBy)
            {
                case SortBy.New:
                    courses = courses.OrderByDescending(m => m.CreatedTimeStamp);
                    break;
                case SortBy.Popular:
                    // TODO: sort by views 
                    break;
                case SortBy.Relevance:
                    // TODO: sort by comparing views, subscribes, clicks
                    break;
                case SortBy.Alphabetically:
                    courses = courses.OrderBy(m => m.Name);
                    break;
            }
            
            courses = courses.Skip(Offset(request.Page, request.PerPage)).Take(request.PerPage);
            
            return new()
            {
                TotalCount = courseQuery.GetCount(),
                Courses = mapper.Map<CourseDTO[]>(courses)
            };;
        }

        public CourseDTO GetCourseById(Guid id)
        {
            Course course = courseQuery.GetOne(id);

            if (course == null)
            {
                throw new HttpResponseException($"Course with id: {id}, was not found");
            }

            return mapper.Map<CourseDTO>(course);
        }

        public CourseDTO CreateCourse(CourseDTO course)
        {
            var result = courseCommand.Create(mapper.Map<Course>(course));

            if (!result)
            {
                throw new HttpResponseException($"Course with name {course.Name}, cannot be created");
            }
            
            return course;
        }

        public void DeleteCourse(Guid id)
        {
            if (!courseCommand.Delete(id))
            {
                throw new HttpResponseException($"Course with id: {id}, cannot be deleted");
            }
        }

        public CourseDTO EditCourse(Guid id, CourseDTO course)
        {
            var result = courseCommand.Update(id, mapper.Map<Course>(course));

            if (!result)
            {
                throw new HttpResponseException($"Course with id: {id}, cannot be updated");
            }

            return course;
        }
        
        public async Task SubscribeUser(string token, Guid courseId, DateTime dateTime)
        {
            var userId = jwtService.Verify(token).Issuer;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new HttpResponseException($"User was not found");
            }

            var course = courseQuery.GetOne(courseId);

            if (course == null)
            {
                throw new HttpResponseException($"Course with id: {courseId}, was not found");
            }

            if (!userCommand.SubscribeCourse(user, course))
            {
                throw new HttpResponseException($"Current user cannot be subscribed, on course with id: {courseId}");
            }
            
            jobService.ScheduleCourseReminder(mapper.Map<UserDTO>(user), mapper.Map<CourseDTO>(course), dateTime);
        }
    }
}