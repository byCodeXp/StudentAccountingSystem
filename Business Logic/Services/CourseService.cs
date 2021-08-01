using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Models;
using Data_Access_Layer.Queries;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Errors;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Business_Logic.Services
{
    public class CourseService
    {
        private readonly DataContext _context;
        private readonly CourseQuery _query;
        private readonly CourseCommand _command;

        public CourseService(DataContext context)
        {
            _context = context;
            _query = new CourseQuery(context);
            _command = new CourseCommand(context);
        }

        public IEnumerable<CourseDTO> GetCourses(int page, int perPage)
        {
            int offset = page <= 1 ? 0 : page * perPage - perPage;

            foreach (var course in _query.GetAll())
            {
                yield return new CourseDTO
                {
                    Name = course.Name,
                    Description = course.Description
                };
            }
        }

        public async Task<CourseDTO> FindCourseAsync(string id)
        {
            Guid guid = Guid.Parse(id);

            Course course = await _query.GetOne(guid);

            if (course != null)
            {
                return new CourseDTO
                {
                    Name = course.Name,
                    Description = course.Description
                };
            }

            throw new HttpResponseException("Not found");
        }

        public async Task<CourseDTO> CreateCourseAsync(CourseDTO courseDTO)
        {
            var course = await _command.CreateAsync(new Course { Name = courseDTO.Name, Description = courseDTO.Description });

            return new CourseDTO
            {
                Name = course.Name,
                Description = course.Description
            };
        }

        public async Task<HttpStatusCode> DeleteCourseAsync(string id)
        {
            Guid guid = Guid.Parse(id);
            return await _command.DeleteAsync(guid);
        }

        public async Task<CourseDTO> EditCourseAsync(string id, CourseDTO courseDTO)
        {
            Guid guid = Guid.Parse(id);
            var course = await _command.UpdateAsync(guid, new Course { Name = courseDTO.Name, Description = courseDTO.Description });
            return new CourseDTO { Name = course.Name, Description = courseDTO.Description };
        }
    }
}
