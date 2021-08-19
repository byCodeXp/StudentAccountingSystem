using System;
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
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("Page/{page}")]
        public IEnumerable<CourseDTO> Page(int? page)
        {
            int perPage = 12;
            return _courseService.GetCourses(page ?? 1, perPage);
        }

        [HttpGet("Details/{id}")]
        public async Task<CourseDTO> Course(Guid id)
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

        [HttpDelete("Delete")]
        [Authorize(AppEnv.Roles.Admin)]
        public async Task<HttpStatusCode> Delete(Guid id)
        {
            return await _courseService.DeleteCourseAsync(id);
        }

        [HttpPut("Edit")]
        [Authorize(AppEnv.Roles.Admin)]
        public async Task<CourseDTO> Edit(Guid id, CourseDTO course)
        {
            return await _courseService.EditCourseAsync(id, course);
        }
    }
}
