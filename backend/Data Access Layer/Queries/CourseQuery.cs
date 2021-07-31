using System.Linq;
using System.Threading.Tasks;
using Data_Access_Layer.Models;
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
            return context.Courses;
        }

        public async Task<Course> GetAsync(int id)
        {
            return await context.Courses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await context.Courses.CountAsync();
        }
    }
}