using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business_Logic.ViewModels;
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
        private readonly IMapper mapper;

        public CourseService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            courseCommand = new CourseCommand(this.context);
            courseQuery = new CourseQuery(this.context);
        }
        
        public async Task CreateCourseAsync(CourseDTO course)
        {
        await courseCommand.AddAsync(mapper.Map<Course>(course));
        }
        
        public async Task EditCourseAsync(int id, CourseDTO course)
        {
        await courseCommand.UpdateAsync(id, mapper.Map<Course>(course));
        }
        
        public async Task RemoveCourseAsync(int id)
        {
        await courseCommand.RemoveAsync(id);
        }
        
        public async Task<CourseVM> GetCoursesAsync(int page, int perPage)
        {
            return new()
            {
                TotalCount = await courseQuery.GetCountAsync(),
                Courses = mapper.Map<IEnumerable<CourseDTO>>(courseQuery.GetAll())
            };
        }
        
        public async Task<CourseDTO> GetCourseAsync(int id)
        {
            return mapper.Map<CourseDTO>(await courseQuery.GetAsync(id));
        }
        
        public async Task<int> CountAsync()
        {
            return await courseQuery.GetCountAsync();
        }
    }
}