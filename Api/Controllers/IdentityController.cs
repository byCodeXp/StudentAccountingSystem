using Business_Logic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Data_Transfer_Objects.Requests;

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

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            await identityService.ConfirmEmail(email, token);
            return Ok();
        }
    }
}
