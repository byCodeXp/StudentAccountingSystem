using System.Threading.Tasks;
using API;
using Business_Logic.ViewModels;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Identity;

namespace Business_Logic
{
    public class UserService
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(UserManager<User> userManager, DataContext context, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task RegisterAsync(RegisterRequest model)
        {
            if (!await roleManager.RoleExistsAsync(ENV.Role.Customer))
            {
                await roleManager.CreateAsync(new IdentityRole(ENV.Role.Customer));
            }
            if (!await roleManager.RoleExistsAsync(ENV.Role.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(ENV.Role.Admin));
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, ENV.Role.Customer);
            }
        }

        public async Task<UserToken> LoginAsync(LoginRequest model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            const jwt = J

            return new UserToken
            {
                Token = ,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}