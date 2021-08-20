using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity;
using Data_Transfer_Objects;
using AutoMapper;

namespace Business_Logic.Services
{
    public class UserService
    {
        private readonly ILogger<IdentityService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly JwtService _jwtService;
        private readonly IMapper _mapper;

        public UserService(ILogger<IdentityService> logger, UserManager<User> userManager, JwtService jwtService, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(int page, int perPage)
        {
            int offset = page <= 1 ? 0 : page * perPage - perPage;

            var users = await _userManager.GetUsersInRoleAsync(AppEnv.Roles.Customer); // Get only customer users

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

                var userDto = _mapper.Map<UserDTO>(user);

                userDto.Role = userRole;

                userDtoSet.Add(userDto);
            }

            return userDtoSet;
        }
    }
}
