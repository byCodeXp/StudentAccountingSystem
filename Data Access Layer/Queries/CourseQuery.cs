using System;
using Data_Access_Layer.Models;
using System.Linq;

namespace Data_Access_Layer.Queries
{
    public class CourseQuery
    {
        private readonly DataContext _context;
        

        public CourseQuery(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Course> GetAll()
        {
            return _context.Courses;
        }

        public Course GetOne(Guid id)
        {
            return _context.Courses.Find(id);
        }

        public int GetCount()
        {
            return _context.Courses.Count();
        }
    }
}
