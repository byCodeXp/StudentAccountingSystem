using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity;
using Data_Transfer_Objects;

namespace Business_Logic.Services
{
    public class UserService
    {
        private readonly ILogger<IdentityService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtService _jwtService;

        public UserService (
            ILogger<IdentityService> logger,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            JwtService jwtService)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(int page, int perPage)
        {
            // TODO: Add auto mapper

            int offset = page <= 1 ? 0 : page * perPage - perPage;

            var users = await _userManager.GetUsersInRoleAsync(AppEnv.Roles.Admin);

            var userDtoSet = new List<UserDTO>();

            foreach (var user in users)
            {
                var userRole = "";

                if (await _userManager.IsInRoleAsync(user, AppEnv.Roles.Admin))
                {
                    userRole = AppEnv.Roles.Admin;
                }
                if (await _userManager.IsInRoleAsync(user, AppEnv.Roles.Customer))
                {
                    userRole = AppEnv.Roles.Customer;
                }

                userDtoSet.Add(new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Age = user.Age,
                    Email = user.Email,
                    Role = userRole
                });
            }

            return userDtoSet;
        }
    }
}
