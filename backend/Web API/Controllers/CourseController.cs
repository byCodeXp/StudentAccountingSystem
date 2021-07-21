using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web_API.Data_Access_Layer;
using Web_API.Data_Access_Layer.Models;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : Controller
    {
        private readonly ApplicationDataContext context;

        public CourseController(ApplicationDataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Course> Get(int page, int perPage)
        {
            return context.Courses.Skip(perPage * page).Take(page);
        }
    }
}