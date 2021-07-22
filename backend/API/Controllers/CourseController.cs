using System.Linq;
using System.Threading.Tasks;
using API.ViewModels;
using Business_Logic;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService courseService;
        
        public CourseController(CourseService courseService)
        {
            this.courseService = courseService;
        }
        
        [HttpGet]
        [Route("All")]
        public async Task<CourseVM> GetCourses(int page, int perPage)
        {
            int offset = page <= 1 ? 0 : page * perPage - perPage;
            
            return new CourseVM()
            {
                TotalCount = await courseService.CountAsync(),
                Courses = courseService.GetCourses().Skip(offset).Take(perPage)
            };
        }

        [HttpGet]
        [Route("One")]
        public async Task<CourseDTO> GetCourse(int id)
        {
            return await courseService.GetCourseAsync(id);
        }
    }
}