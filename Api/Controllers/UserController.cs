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
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("page")]
        public IActionResult Get(GetPageRequest request)
        {
            return Ok(_userService.GetUsers(request));
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_userService.GetUserById(id));
        }
        
        [HttpPost("subscribe")] 
        public async Task<IActionResult> SubscribeOnCourse(Guid courseId, DateTime date)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            await _userService.SubscribeUserOnCourseAsync(courseId, token);
            return Ok();
        }
    }
}
