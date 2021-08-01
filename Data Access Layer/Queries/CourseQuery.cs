using Data_Access_Layer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Course> GetOne(Guid id)
        {
            return await _context.Courses.FindAsync(id);
        }
    }
}
