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

namespace Business_Logic.Services
{
    public class CourseService
    {
        private readonly ILogger<CourseService> _logger;
        private readonly CourseQuery _query;
        private readonly CourseCommand _command;

        public CourseService(DataContext context, ILogger<CourseService> logger)
        {
            _logger = logger;
            _query = new CourseQuery(context);
            _command = new CourseCommand(context);
        }

        public IEnumerable<CourseDTO> GetCourses(int page, int perPage)
        {
            int offset = page <= 1 ? 0 : page * perPage - perPage;

            _logger.LogInformation($"Courses on page: {page}, was returned");

            foreach (var course in _query.GetAll().Skip(offset).Take(perPage))
            {
                yield return new CourseDTO
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    Preview = course.Preview
                };
            }
        }

        public async Task<CourseDTO> FindCourseAsync(Guid id)
        {
            Course course = await _query.GetOne(id);

            if (course != null)
            {
                _logger.LogInformation($"Returned course with id: {course.Id}");

                return new CourseDTO
                {
                    Id = course.Id,
                    Name = course.Name,
                    Description = course.Description,
                    Preview = course.Preview
                };
            }

            throw new HttpResponseException("Not found");
        }

        public async Task<CourseDTO> CreateCourseAsync(CourseDTO courseDTO)
        {
            var course = await _command.CreateAsync(new Course { Name = courseDTO.Name, Description = courseDTO.Description });

            _logger.LogInformation($"Course with id: {course.Id}, was created");

            return new CourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Preview = course.Preview
            };
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
            
            return new CourseDTO { Id = course.Id, Name = course.Name, Description = courseDTO.Description, Preview = course.Preview };
        }
    }
}