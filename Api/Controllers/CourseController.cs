using Business_Logic.Services;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("Page/{page}")]
        public IEnumerable<CourseDTO> Page(int? page)
        {
            int perPage = 12;
            return _courseService.GetCourses(page ?? 1, perPage);
        }

        [HttpPost("Details/{id}")]
        public async Task<CourseDTO> Course(string id)
        {
            return await _courseService.FindCourseAsync(id);
        }

        [HttpPost("Create")]
        [Authorize(AppEnv.Roles.Admin)]
        public async Task<CourseDTO> Create(CourseDTO course)
        {
            // TODO: ModelState validation
            return await _courseService.CreateCourseAsync(course);
        }

        [HttpPost("Delete")]
        [Authorize(AppEnv.Roles.Admin)]
        public async Task<HttpStatusCode> Delete(string id)
        {
            return await _courseService.DeleteCourseAsync(id);
        }

        [HttpPost("Edit")]
        [Authorize(AppEnv.Roles.Admin)]
        public async Task<CourseDTO> Edit(string id, CourseDTO course)
        {
            return await _courseService.EditCourseAsync(id, course);
        }
    }
}
