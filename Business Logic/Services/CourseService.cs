using System;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Models;
using Data_Access_Layer.Queries;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Errors;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CourseService> _logger;
        private readonly JwtService _jwtService;
        private readonly CourseQuery _courseQuery;
        private readonly CourseCommand _courseCommand;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CourseService(DataContext context, ILogger<CourseService> logger, IMapper mapper, JwtService jwtService, UserManager<User> userManager)
        {
            _logger = logger;
            _courseQuery = new CourseQuery(context);
            _courseCommand = new CourseCommand(context);
            _mapper = mapper;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public IEnumerable<CourseDTO> GetCourses(int page, int perPage)
        {
            int offset = page <= 1 ? 0 : page * perPage - perPage;

            _logger.LogInformation($"Courses on page: {page}, was returned");

            var courses = _courseQuery.GetAll().OrderBy(m => m.CreatedTimeStamp).Skip(offset).Take(perPage);

            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        public CourseDTO GetCourseById(Guid id)
        {
            Course course = _courseQuery.GetOne(id);

            if (course == null)
            {
                throw new HttpResponseException("Not found");
            }

            _logger.LogInformation($"Returned course with id: {course.Id}");

            return _mapper.Map<CourseDTO>(course);
        }

        public CourseDTO CreateCourse(CourseDTO course)
        {
            var result = _courseCommand.Create(_mapper.Map<Course>(course));

            if (!result)
            {
                throw new HttpResponseException("Bad request!");
            }
            
            _logger.LogInformation($"Course with name: {course.Name}, was created");

            return course;
        }

        public void DeleteCourse(Guid id)
        {
            _logger.LogInformation($"Course with id: {id}, was deleted");

            if (!_courseCommand.Delete(id))
            {
                throw new HttpResponseException("Bad request!");
            }
        }

        public CourseDTO EditCourse(Guid id, CourseDTO course)
        {
            var result = _courseCommand.Update(id, _mapper.Map<Course>(course));

            if (!result)
            {
                throw new HttpResponseException("Bad request!");
            }

            return course;
        }
        
        public async Task<HttpStatusCode> SubscribeUser(string token, Guid courseId)
        {
            var userId = _jwtService.Verify(token).Issuer;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return HttpStatusCode.NotFound;
            }

            var course = _courseQuery.GetOne(courseId);

            if (course == null)
            {
                return HttpStatusCode.NotFound;
            }

            if (!user.SubscribedCourses.Contains(course))
            {
                user.SubscribedCourses.Add(course);

            }
            else
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.OK;
        }
    }
}