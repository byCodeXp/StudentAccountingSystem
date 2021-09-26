using System;
using System.Security.Claims;
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
        private readonly CourseService courseService;

        public CourseController(CourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost("get")]
        public IActionResult Get([FromBody] CoursesRequest request)
        {
            return Ok(courseService.GetCourses(request));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            return Ok(courseService.GetOneCourse(id, token));
        }

        [HttpGet("user/{id}/courses")]
        public IActionResult GetUserCourses(string id)
        {
            return Ok(courseService.GetCoursesByUserId(id));
        }

        [HttpPost("create")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Create([FromBody] CourseDTO course)
        {
            return Ok(courseService.CreateCourse(course));
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Delete(Guid id)
        {
            courseService.DeleteCourse(id);
            return Ok();
        }

        [HttpPut("update")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Update([FromBody] CourseDTO course)
        {
            return Ok(courseService.EditCourse(course));
        }
    }
}
