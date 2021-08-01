using Data_Access_Layer.Models;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Services
{
    public class IdentityService
    {
        private readonly ILogger<IdentityService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtService _jwtService;
        private readonly EmailService _emailService;

        public IdentityService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            JwtService jwtService,
            EmailService emailService,
            ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<HttpStatusCode> RegisterAsync(RegisterRequest request)
        {
            await RolesEnsureCreate(AppEnv.Roles.Master, AppEnv.Roles.Admin, AppEnv.Roles.Customer);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                Age = request.Age
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new HttpResponseException("Invalid credentials", result.Errors);
            }
            
            await _userManager.AddToRoleAsync(user, AppEnv.Roles.Customer);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token)); 

            var link = $"https://localhost:5001/api/Identity/ConfirmEmail?email=" + user.Email + "&token=" + token;

            await _emailService.SendMailAsync("Succesfuly registration", new EmailAddress(user.Email), $"Your activation link: {link}");

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

            // TODO: Add refresh token

            return _jwtService.WriteToken(user);
        }

        public async Task<HttpStatusCode> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new HttpResponseException("Not found");
            }

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            _logger.LogInformation($"Result: {result}\n");

            if (result.Succeeded)
            {
                return HttpStatusCode.OK;
            }

            throw new HttpResponseException("Not confirmed", result.Errors);
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
