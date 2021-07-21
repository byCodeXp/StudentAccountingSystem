using System.Collections.Generic;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Models;
using Data_Access_Layer.Queries;
using Data_Transfer_Objects;

namespace Business_Logic
{
    public class CourseService
    {
        private readonly DataContext context;
        private readonly CourseCommand courseCommand;
        private readonly CourseQuery courseQuery;

        public CourseService(DataContext context)
        {
            this.context = context;
            courseCommand = new CourseCommand(this.context);
            courseQuery = new CourseQuery(this.context);
        }

        public async Task CreateCourseAsync(CourseDTO course)
        {
            await courseCommand.AddAsync(new ()
            {
                Name = course.Name, StartDate = course.StartDate, FinishDate = course.FinishDate
            });
        }

        public async Task EditCourseAsync(int id, CourseDTO course)
        {
            await courseCommand.UpdateAsync(id, new ()
            {
                Name = course.Name, StartDate = course.StartDate, FinishDate = course.FinishDate
            });
        }

        public async Task RemoveCourseAsync(int id)
        {
            await courseCommand.RemoveAsync(id);
        }

        public IEnumerable<CourseDTO> GetCourses()
        {
            foreach (Course course in courseQuery.GetAll())
            {
                yield return new()
                {
                    Name = course.Name, StartDate = course.StartDate, FinishDate = course.FinishDate
                };
            }
        }

        public async Task<CourseDTO> GetCourseAsync(int id)
        {
            Course course = await courseQuery.GetAsync(id);
            return new()
            {
                Name = course.Name, StartDate = course.StartDate, FinishDate = course.FinishDate
            };
        }

        public async Task<int> CountAsync()
        {
            return await courseQuery.GetCountAsync();
        }
    }
}