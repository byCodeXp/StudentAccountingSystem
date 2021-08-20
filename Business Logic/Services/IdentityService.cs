using AutoMapper;
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
        private readonly JwtService _jwtService;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;

        public IdentityService(
            ILogger<IdentityService> logger,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            JwtService jwtService,
            EmailService emailService, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<HttpStatusCode> RegisterAsync(RegisterRequest request)
        {
            var user = _mapper.Map<User>(request);

            var dentityResult = await _userManager.CreateAsync(user, request.Password);

            if (!dentityResult.Succeeded)
            {
                throw new HttpResponseException("Invalid credentials", dentityResult.Errors);
            }

            _logger.LogInformation($"User created with id: {user.Id}");
            
            var result = await _userManager.AddToRoleAsync(user, AppEnv.Roles.Customer);

            if (!result.Succeeded)
            {
                throw new HttpResponseException("Server error", dentityResult.Errors);
            }

            _logger.LogInformation($"Role added to user");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token)); 

            var link = $"https://localhost:5001/api/Identity/ConfirmEmail?email={user.Email}&token={token}";

            await _emailService.SendMailAsync("Successfully registration", new EmailAddress(user.Email), $"Your activation link: {link}");

            _logger.LogInformation($"Confirmation link was send on email address: {user.Email}");

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

            _logger.LogInformation($"Successfully user authentication with id: {user.Id}");

            // TODO: Add refresh token

            string userRole = "";

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

            return _jwtService.WriteToken(userDto);
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

            if (result.Succeeded)
            {
                _logger.LogInformation($"Successfully user email confirmation with id: {user.Id}");

                return HttpStatusCode.OK;
            }

            throw new HttpResponseException("Not confirmed", result.Errors);
        }
    }
}
