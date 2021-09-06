﻿using System;
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

        [HttpGet("get")]
        public IActionResult Get([FromQuery] GetCoursesRequest request)
        {
            return Ok(courseService.GetCourses(request));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(courseService.GetCourseById(id));
        }

        [HttpPost("create")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Create([FromBody] CourseDTO course)
        {
            return Ok(courseService.CreateCourse(course));
        }

        [HttpDelete("delete")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Delete(Guid id)
        {
            courseService.DeleteCourse(id);
            return Ok();
        }

        [HttpPut("update")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Update(Guid id, [FromBody] CourseDTO course)
        {
            return Ok(courseService.EditCourse(id, course));
        }
    }
}
