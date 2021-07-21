using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web_API.Data_Access_Layer;
using Web_API.Data_Access_Layer.Models;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDataContext context;

        public UserController(ApplicationDataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<AppUser> Get(int page, int perPage)
        {
            return context.Users.Skip(perPage * page).Take(page);
        }
    }
}