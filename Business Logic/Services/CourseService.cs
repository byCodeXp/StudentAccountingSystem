﻿using System;
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
        private readonly CourseQuery _query;
        private readonly CourseCommand _command;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CourseService(DataContext context, ILogger<CourseService> logger, IMapper mapper, JwtService jwtService, UserManager<User> userManager)
        {
            _logger = logger;
            _query = new CourseQuery(context);
            _command = new CourseCommand(context);
            _mapper = mapper;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<HttpStatusCode> SubscribeUser(string token, Guid courseId)
        {
            var userId = _jwtService.Verify(token).Issuer;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return HttpStatusCode.NotFound;
            }

            var course = await _query.GetOne(courseId);

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

        public IEnumerable<CourseDTO> GetCourses(int page, int perPage)
        {
            int offset = page <= 1 ? 0 : page * perPage - perPage;

            _logger.LogInformation($"Courses on page: {page}, was returned");

            var courses = _query.GetAll().OrderBy(m => m.CreatedTimeStamp).Skip(offset).Take(perPage);

            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        public async Task<CourseDTO> FindCourseAsync(Guid id)
        {
            Course course = await _query.GetOne(id);

            if (course != null)
            {
                _logger.LogInformation($"Returned course with id: {course.Id}");

                return _mapper.Map<CourseDTO>(course);
            }

            throw new HttpResponseException("Not found");
        }

        public async Task<CourseDTO> CreateCourseAsync(CourseDTO courseDTO)
        {
            var course = await _command.CreateAsync(new Course { Name = courseDTO.Name, Description = courseDTO.Description });

            _logger.LogInformation($"Course with id: {course.Id}, was created");

            return _mapper.Map<CourseDTO>(course);
        }

        public async Task<HttpStatusCode> DeleteCourseAsync(Guid id)
        {
            _logger.LogInformation($"Course with id: {id}, was deleted");

            return await _command.DeleteAsync(id);
        }

        public async Task<CourseDTO> EditCourseAsync(Guid id, CourseDTO courseDTO)
        {
            var course = await _command.UpdateAsync(id, new Course { Name = courseDTO.Name, Description = courseDTO.Description });

            _logger.LogInformation($"Course with id: {id}, was updated");

            return _mapper.Map<CourseDTO>(course);
        }
    }
}