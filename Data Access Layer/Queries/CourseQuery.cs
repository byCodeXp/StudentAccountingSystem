using System;
using System.Collections.Generic;
using Data_Access_Layer.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Queries
{
    public class CourseQuery
    {
        private readonly DataContext context;

        public CourseQuery(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<Course> GetAll()
        {
            return context.Courses.Include(m => m.Categories);
        }

        public Course GetOne(Guid id)
        {
            return context.Courses.Include(m => m.Categories).FirstOrDefault(m => m.Id == id);
        }

        public IQueryable<Course> Search(string query)
        {
            return context.Courses.Where(m => m.Name.Contains(query));
        }

        public IEnumerable<Course> GetCoursesByUserId(string id)
        {
            var user = context.Users.Find(id);
            
            if (user == null)
            {
                return null;
            }

            return context.UsersCourses.Where(m => m.User == user).Select(m => m.Course);
        }
    }
}
