using Data_Access_Layer.Models;
using System;
using System.Linq;

namespace Data_Access_Layer.Commands
{
    public class CourseCommand
    {
        private readonly DataContext _context;

        public CourseCommand(DataContext context)
        {
            _context = context;
        }

        public bool Create(Course course)
        {
            if (_context.Courses.Any(m => m.Name == course.Name))
            {
                return false;
            }
            
            course.CreatedTimeStamp = DateTime.Now;
            course.UpdatedTimeStamp = DateTime.Now;

            // TODO: make logging
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(Guid id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
            {
                return false;
            }

            try
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Guid id, Course course)
        {
            var courseUpdate = _context.Courses.Find(id);

            if (courseUpdate == null)
            {
                return false;
            }

            courseUpdate.Name = course.Name;
            courseUpdate.Description = course.Description;
            course.UpdatedTimeStamp = DateTime.Now;

            try
            {
                _context.Courses.Update(courseUpdate);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
