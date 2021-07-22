using System.Threading.Tasks;
using Data_Access_Layer.Models;
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

        public async Task AddAsync(Course course)
        {
            await context.Courses.AddAsync(course);
        }

        public async Task UpdateAsync(int id, Course newCourse)
        {
            Course course = await context.Courses.FindAsync(id);
            
            course.Name = string.IsNullOrEmpty(newCourse.Name) ? newCourse.Name : course.Name;
            course.StartDate = newCourse.StartDate;
            course.FinishDate = newCourse.FinishDate;
            
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            Course course = await context.Courses.FirstOrDefaultAsync();
            if (course != null)
            {
                context.Courses.Remove(course);
            }
        }
    }
}