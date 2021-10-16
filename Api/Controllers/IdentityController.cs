using Business_Logic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business_Logic.Extensions;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Requests;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService identityService;

        public IdentityController(IdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await identityService.RegisterAsync(request);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await identityService.LoginAsync(request));
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginRequest request)
        {
            return Ok(await identityService.FacebookLogin(request));
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            var userId = HttpContext.GetUserId();
            return Ok(identityService.GetUser(userId));
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            await identityService.ConfirmEmail(email, token);
            return Ok();
        }

        [HttpPut("update-personal-data")]
        [Authorize(Roles = AppEnv.Roles.Customer + ", " + AppEnv.Roles.Admin)]
        public IActionResult UpdatePersonalData([FromBody] ChangeProfileRequest request)
        {
            var userId = HttpContext.GetUserId();
            return Ok(identityService.ChangePersonalData(userId, request));
        }

        [HttpPut("change-password")]
        [Authorize(Roles = AppEnv.Roles.Customer + ", " + AppEnv.Roles.Admin)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = HttpContext.GetUserId();
            await identityService.ChangePasswordAsync(userId, request);
            return Ok();
        }
    }
}
