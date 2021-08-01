using Data_Access_Layer.Models;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Errors;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading.Tasks;

namespace Business_Logic.Services
{
    public class IdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<HttpStatusCode> RegisterAsync(RegisterRequest request)
        {
            await RolesEnsureCreate(AppEnv.Roles.Master, AppEnv.Roles.Admin, AppEnv.Roles.Customer);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Age = request.Age
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new HttpResponseException("Invalid credentials", result.Errors);
            }

            await _userManager.AddToRoleAsync(user, AppEnv.Roles.Customer);

            return HttpStatusCode.Created;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new HttpResponseException("Invalid credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                throw new HttpResponseException("Invalid credentials");
            }

            // TODO: Make jwt authentication

            return "";
        }

        private async Task RolesEnsureCreate(params string[] roles)
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
