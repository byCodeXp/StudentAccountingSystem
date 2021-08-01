using Business_Logic.Services;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService _identityService;

        public IdentityController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Register")]
        public async Task<HttpStatusCode> Register(RegisterRequest request)
        {
            return await _identityService.RegisterAsync(request);
        }

        [HttpPost("Login")]
        public async Task<string> Login(LoginRequest request)
        {
            return await _identityService.LoginAsync(request);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<HttpStatusCode> ConfirmEmail(string email, string token)
        {
            return await _identityService.ConfirmEmail(email, token);
        }
    }
}
