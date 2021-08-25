using AutoMapper;
using Data_Access_Layer.Models;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Data_Transfer_Objects.Requests;

namespace Business_Logic.Services
{
    public class IdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtService _jwtService;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;

        public IdentityService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            JwtService jwtService,
            EmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var user = _mapper.Map<User>(request);

            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                throw new HttpResponseException("Invalid credentials", identityResult.Errors) { HttpStatusCode = HttpStatusCode.BadRequest };
            }
            
            await _userManager.AddToRoleAsync(user, AppEnv.Roles.Customer);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var link = $"https://localhost:5001/api/Identity/ConfirmEmail?email={user.Email}&token={token}";

            await _emailService.SendMailAsync("Successfully registration", new EmailAddress(user.Email), $"Your activation link: {link}");
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new HttpResponseException("Invalid credentials") { HttpStatusCode = HttpStatusCode.BadRequest };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                throw new HttpResponseException("Invalid credentials") { HttpStatusCode = HttpStatusCode.BadRequest };
            }

            // TODO: Add refresh token

            string userRole = "";

            if (await _userManager.IsInRoleAsync(user, AppEnv.Roles.Customer))
            {
                userRole = AppEnv.Roles.Customer;
            }
            if (await _userManager.IsInRoleAsync(user, AppEnv.Roles.Admin))
            {
                userRole = AppEnv.Roles.Admin;
            }

            var userDto = _mapper.Map<UserDTO>(user);

            userDto.Role = userRole;

            return _jwtService.WriteToken(userDto);
        }

        public async Task ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new HttpResponseException("Invalid credentials") { HttpStatusCode = HttpStatusCode.BadRequest };
            }

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            var result = await _userManager.ConfirmEmailAsync(user, codeDecoded);

            if (!result.Succeeded)
            {
                throw new HttpResponseException("Email was not confirmed", result.Errors) { HttpStatusCode = HttpStatusCode.BadRequest };
            }
        }
    }
}
