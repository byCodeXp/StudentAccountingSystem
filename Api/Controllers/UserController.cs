using System;
using Business_Logic.Services;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppEnv.Roles.Admin)]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("page/{page}")]
        public IActionResult Get(int? page)
        {
            return Ok(_userService.GetUsers(page ?? 1, 10));
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_userService.GetUserById(id));
        }
        
        [HttpPost("subscribe")] 
        public async Task<IActionResult> SubscribeOnCourse(Guid courseId, string token)
        {
            await _userService.SubscribeUserOnCourseAsync(courseId, token);
            return Ok();
        }
    }
}
