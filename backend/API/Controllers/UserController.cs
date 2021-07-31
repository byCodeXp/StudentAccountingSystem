using System.Threading.Tasks;
using Api;
using Business_Logic;
using Business_Logic.ViewModels;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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

        [HttpPost("Register")]
        public async Task Register(RegisterRequest model)
        {
            await userService.RegisterAsync(model);
        }

        [HttpPost("Login")]
        public async Task<UserToken> Login(LoginRequest model)
        {
            return await userService.LoginAsync(model);
        }
    }
}