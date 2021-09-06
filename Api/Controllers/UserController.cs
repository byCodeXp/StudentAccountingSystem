using System;
using System.Linq;
using Business_Logic.Services;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Data_Transfer_Objects.Requests;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppEnv.Roles.Admin)]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet("get")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Get([FromQuery] GetUsersRequest request)
        {
            return Ok(userService.GetUsers(request));
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Get(Guid id)
        {
            return Ok(userService.GetUserById(id));
        }
        
        [HttpPost("subscribe")] 
        public async Task<IActionResult> SubscribeOnCourse(Guid courseId, DateTime date)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            await userService.SubscribeUserOnCourseAsync(courseId, token);
            return Ok();
        }
    }
}
