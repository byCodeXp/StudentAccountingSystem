using System.Threading.Tasks;
using Business_Logic;
using Business_Logic.ViewModels;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService courseService;
        
        public CourseController(CourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet("Page/{page}")]
        public async Task<CourseVM> GetAll(int page)
        {
            return await courseService.GetCoursesAsync(page, 12);
        }

        [HttpGet("{id}")]
        public async Task<CourseDTO> Get(int id)
        {
            return await courseService.GetCourseAsync(id);
        }
    }
}