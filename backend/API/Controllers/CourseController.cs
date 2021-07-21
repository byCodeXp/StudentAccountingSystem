using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IEnumerable<CourseDTO> GetCourses(int page, int perPage)
        {
            int offset = page <= 1 ? 0 : page * perPage - perPage;
            return courseService.GetCourses().Skip(offset).Take(perPage);
        }

        [HttpPost]
        public async Task<CourseDTO> GetCourse(int id)
        {
            return await courseService.GetCourseAsync(id);
        }
    }
}