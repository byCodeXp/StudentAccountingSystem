using System;
using Business_Logic.Services;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Entities;
using Data_Transfer_Objects.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("page")]
        public IActionResult Get(GetPageRequest request)
        {
            return Ok(_courseService.GetCourses(request));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_courseService.GetCourseById(id));
        }

        [HttpPost("create")]
        [Authorize(AppEnv.Roles.Admin)]
        public IActionResult Create([FromBody] CourseDTO course)
        {
            // TODO: ModelState validation
            return Ok(_courseService.CreateCourse(course));
        }

        [HttpDelete("delete")]
        [Authorize(AppEnv.Roles.Admin)]
        public IActionResult Delete(Guid id)
        {
            _courseService.DeleteCourse(id);
            return Ok();
        }

        [HttpPut("update")]
        [Authorize(AppEnv.Roles.Admin)]
        public IActionResult Update(Guid id, [FromBody] CourseDTO course)
        {
            return Ok(_courseService.EditCourse(id, course));
        }
    }
}
