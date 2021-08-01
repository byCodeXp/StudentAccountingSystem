using Data_Access_Layer.Models;
using Data_Transfer_Objects.Errors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Data_Access_Layer.Commands
{
    public class CourseCommand
    {
        private readonly DataContext _context;

        public CourseCommand(DataContext context)
        {
            _context = context;
        }

        public async Task<Course> CreateAsync(Course course)
        {
            course.CreatedTimeStamp = DateTime.Now;
            course.UpdatedTimeStamp = DateTime.Now;

            var result = await _context.Courses.AddAsync(course);

            if (result.State == EntityState.Added)
            {
                return result.Entity;
            }

            throw new HttpResponseException("Invalid credentials");
        }

        public async Task<HttpStatusCode> DeleteAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return HttpStatusCode.NotFound;
            }

            _context.Courses.Remove(course);
            
            await _context.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<Course> UpdateAsync(Guid id, Course course)
        {
            Course courseUpdate = await _context.Courses.FindAsync(id);

            if (courseUpdate == null)
            {
                throw new HttpResponseException("Not found");
            }

            courseUpdate.Name = course.Name;
            courseUpdate.Description = course.Description;
            course.UpdatedTimeStamp = DateTime.Now;

            _context.Courses.Update(courseUpdate);

            await _context.SaveChangesAsync();

            return courseUpdate;
        }
    }
}
