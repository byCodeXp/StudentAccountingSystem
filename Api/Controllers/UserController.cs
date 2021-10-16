using System;
using Business_Logic.Services;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business_Logic.Extensions;
using Data_Transfer_Objects.Requests;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("get")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Get([FromQuery] UsersRequest request)
        {
            return Ok(userService.GetUsers(request));
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Get(string id)
        {
            return Ok(userService.GetUserById(id));
        }

        [HttpGet("courses")]
        [Authorize(Roles = AppEnv.Roles.Customer + ", " + AppEnv.Roles.Admin)]
        public async Task<IActionResult> GetUserCourses()
        {
            var userId = HttpContext.GetUserId();
            return Ok(await userService.GetUserCourses(userId));
        }
        
        [HttpPost("subscribe")] 
        [Authorize(Roles = AppEnv.Roles.Customer + ", " + AppEnv.Roles.Admin)]
        public async Task<IActionResult> SubscribeCourse([FromBody] SubscribeRequest request)
        {
            var userId = HttpContext.GetUserId();
            await userService.SubscribeCourseAsync(request, userId);
            return Ok();
        }

        [HttpDelete("unsubscribe/{courseId}")]
        [Authorize(Roles = AppEnv.Roles.Customer + ", " + AppEnv.Roles.Admin)]
        public async Task<IActionResult> UnsubscribeCourse(Guid courseId)
        {
            var userId = HttpContext.GetUserId();
            await userService.UnsubscribeFromCourse(courseId, userId);
            return Ok();
        }
    }
}
