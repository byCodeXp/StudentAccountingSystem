using System;
using System.Collections.Generic;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Models;
using Data_Access_Layer.Queries;
using Business_Logic.Exceptions;
using System.Linq;
using AutoMapper;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Entities;
using Data_Transfer_Objects.Requests;
using Data_Transfer_Objects.ViewModels;
using Microsoft.Extensions.Logging;

namespace Business_Logic.Services
{
    public class CourseService
    {
        private readonly ILogger<CourseService> logger;
        private readonly CategoryQuery categoryQuery;
        private readonly CourseCommand courseCommand;
        private readonly CourseQuery courseQuery;
        private readonly UserQuery userQuery;
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CourseService(DataContext context, IMapper mapper, ILogger<CourseService> logger)
        {
            courseCommand = new(context);
            categoryQuery = new(context);
            courseQuery = new(context);
            userQuery = new(context);
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        private int Offset (int page, int perPage) => page <= 1 ? 0 : page * perPage - perPage;
        
        public CourseVM GetCourses(CoursesRequest request)
        {
            IQueryable<Course> courses = courseQuery.GetAll();

            if (!string.IsNullOrEmpty(request.Search))
            {
                // Search

                courses = courseQuery.Search(request.Search);
            }
            else
            {
                // Filter

                if (request.Categories.Any())
                {
                    courses = categoryQuery
                        .GetAll()
                        .Where(m => request.Categories.Contains(m.Name))
                        .SelectMany(s => s.Courses)
                        .Distinct();
                }
            }
            
            // Sorting
            
            switch (request.SortBy)
            {
                case SortBy.New:
                    courses = courses.OrderByDescending(m => m.CreatedTimeStamp);
                    break;
                case SortBy.Popular:
                    courses = courses.OrderByDescending(m => m.Views);
                    break;
                case SortBy.Relevance:
                    courses = courses.OrderByDescending(m => m.Views).ThenByDescending(m => m.CreatedTimeStamp);
                    break;
                case SortBy.Alphabetically:
                    courses = courses.OrderBy(m => m.Name);
                    break;
            }
            
            // Total count
            
            int totalCount = courses.Count();
            
            // Pagination
            
            courses = courses.Skip(Offset(request.Page, request.PerPage)).Take(request.PerPage);
            
            logger.LogInformation($"Was returned {courses.Count()} courses");
            
            return new CourseVM
            {
                TotalCount = totalCount,
                Courses = mapper.Map<List<CourseDTO>>(courses.ToList())
            };;
        }

        public List<CourseDTO> GetCoursesByUserId(string id)
        {
            var courses = courseQuery.GetCoursesByUserId(id);
            return mapper.Map<List<CourseDTO>>(courses);
        }

        public CourseDTO GetCourseAndIncrementViewCount(Guid courseId, string userId)
        {
            Course course = courseQuery.GetOne(courseId);

            if (course == null)
            {
                throw new NotFoundRestException($"Course with id: {courseId}, was not found");
            }

            var courseDto = mapper.Map<CourseDTO>(course);

            if (!string.IsNullOrEmpty(userId))
            {
                var user = userQuery.GetById(userId);
                courseDto.Subscribed = userQuery.UserExistsCourse(user, course);
            }
            else
            {
                courseDto.Subscribed = false;
            }
            
            course.Views++;

            courseCommand.Update(course);

            context.SaveChanges();
            
            return courseDto;
        }

        public CourseDTO CreateCourse(CourseDTO courseDto)
        {
            var course = mapper.Map<Course>(courseDto);

            course.Categories = new List<Category>(categoryQuery.GetAll().Where(m => course.Categories.Contains(m))); // TODO: MAKE as Query
            
            var result = courseCommand.Create(course);

            if (!result)
            {
                throw new BadRequestRestException($"Course with name {course.Name}, cannot be created");
            }

            context.SaveChanges();
            
            return courseDto;
        }

        public void DeleteCourse(Guid id)
        {
            if (!courseCommand.Delete(id))
            {
                throw new BadRequestRestException($"Course with id: {id}, cannot be deleted");
            }
            
            context.SaveChanges();
        }

        public CourseDTO EditCourse(CourseDTO course)
        {
            var result = courseCommand.Update(mapper.Map<Course>(course));

            if (!result)
            {
                throw new BadRequestRestException($"Course with id: {course.Id}, cannot be updated");
            }
            
            context.SaveChanges();

            return course;
        }
    }
}