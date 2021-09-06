using Data_Access_Layer.Models;
using System;
using System.Linq;

namespace Data_Access_Layer.Commands
{
    public class CourseCommand
    {
        private readonly DataContext context;

        public CourseCommand(DataContext context)
        {
            this.context = context;
        }

        public bool Create(Course course)
        {
            if (context.Courses.Any(m => m.Name == course.Name))
            {
                return false;
            }
            
            course.CreatedTimeStamp = DateTime.Now;
            course.UpdatedTimeStamp = DateTime.Now;

            // TODO: make logging
            try
            {
                context.Courses.Add(course);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(Guid id)
        {
            var course = context.Courses.Find(id);

            if (course == null)
            {
                return false;
            }

            try
            {
                context.Courses.Remove(course);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Guid id, Course course)
        {
            var courseUpdate = context.Courses.Find(id);

            if (courseUpdate == null)
            {
                return false;
            }

            courseUpdate.Name = course.Name;
            courseUpdate.Description = course.Description;
            course.UpdatedTimeStamp = DateTime.Now;

            try
            {
                context.Courses.Update(courseUpdate);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
