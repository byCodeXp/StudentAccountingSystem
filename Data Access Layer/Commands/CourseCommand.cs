using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            
            try
            {
                context.Courses.Add(course);
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
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Course newCourse)
        {
            var course = context.Courses.Include(m => m.Categories).FirstOrDefault(m => m.Id == newCourse.Id);

            if (course == null)
            {
                return false;
            }

            course.Name = newCourse.Name;
            course.Description = newCourse.Description;
            course.Preview = newCourse.Preview;
            course.Categories = new List<Category>(context.Categories.Where(m => newCourse.Categories.Contains(m)));

            context.Courses.Update(course);
            
            return true;
        }
    }
}
