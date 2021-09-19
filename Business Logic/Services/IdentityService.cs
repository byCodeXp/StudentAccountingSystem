using System.Collections.Generic;
using AutoMapper;
using Data_Access_Layer.Models;
using Data_Transfer_Objects;
using Business_Logic.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;
using Business_Logic.Helpers;
using Business_Logic.Utils;
using Data_Access_Layer;
using Data_Access_Layer.Queries;
using Data_Transfer_Objects.Entities;
using Data_Transfer_Objects.Requests;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace Business_Logic.Services
{
    public class IdentityService
    {
        private readonly IJwtUtility jwtUtility;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly CourseQuery courseQuery;
        private readonly ILogger<IdentityService> logger;
        private readonly EmailService emailService;
        private readonly RazorTemplateHelper razorTemplateHelper;

        public IdentityService(
            IJwtUtility jwtUtility,
            UserManager<User> userManager,
            EmailService emailService, IMapper mapper,
            DataContext context, ILogger<IdentityService> logger, RazorTemplateHelper razorTemplateHelper)
        {
            this.jwtUtility = jwtUtility;
            this.userManager = userManager;
            this.emailService = emailService;
            this.mapper = mapper;
            this.logger = logger;
            this.razorTemplateHelper = razorTemplateHelper;
            courseQuery = new (context);
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var user = mapper.Map<User>(request);
            user.UserName = request.Email;

            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                throw new BadRequestRestException("Invalid credentials", identityResult.Errors);
            }
            
            await userManager.AddToRoleAsync(user, AppEnv.Roles.Customer);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var link = $"https://localhost:5001/api/identity/confirm?email={user.Email}&token={token}";

            var template = await razorTemplateHelper.GetTemplateHtmlAsStringAsync("Confirm", link);
            
            await emailService.SendMailAsync("Successfully registration", new EmailAddress(user.Email), template);

            logger.LogInformation($"Registered user with id {user.Id}");
        }

        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new BadRequestRestException("Invalid credentials");
            }


            var checkPassword = await userManager.CheckPasswordAsync(user, request.Password);

            if (!checkPassword)
            {
                throw new BadRequestRestException("Invalid credentials");
            }

            var checkEmail = await userManager.IsEmailConfirmedAsync(user);

            if (!checkEmail)
            {
                throw new BadRequestRestException("Not confirmed email");
            }
            
            // TODO: Add refresh token

            string userRole = "";

            if (await userManager.IsInRoleAsync(user, AppEnv.Roles.Customer))
            {
                userRole = AppEnv.Roles.Customer;
            }
            if (await userManager.IsInRoleAsync(user, AppEnv.Roles.Admin))
            {
                userRole = AppEnv.Roles.Admin;
            }

            var userDto = mapper.Map<UserDTO>(user);

            userDto.Role = userRole;

            var courses = courseQuery.GetCoursesByUserId(user.Id);

            logger.LogInformation($"Was login user with id {user.Id}");
            
            return new UserResponse
            {
                Token = jwtUtility.GenerateToken(userDto),
                Courses = mapper.Map<List<CourseDTO>>(courses)
            };
        }

        public async Task ConfirmEmail(string email, string emailToken)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new BadRequestRestException("Invalid credentials");
            }

            var codeDecodedBytes = WebEncoders.Base64UrlDecode(emailToken);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

            var result = await userManager.ConfirmEmailAsync(user, codeDecoded);

            if (!result.Succeeded)
            {
                throw new BadRequestRestException("Email was not confirmed", result.Errors);
            }
            
            logger.LogInformation($"User with id {user.Id} confirm email");
        }
    }
}
