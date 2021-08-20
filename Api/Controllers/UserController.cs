using Business_Logic.Services;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("Page/{page}")]
        public async Task<IEnumerable<UserDTO>> Get(int page)
        {
            return await _userService.GetUsers(page, 10);
        }
    }
}
