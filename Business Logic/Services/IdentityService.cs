using System;
using AutoMapper;
using Data_Access_Layer.Models;
using Data_Transfer_Objects;
using Business_Logic.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;
using Business_Logic.Helpers;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Queries;
using Data_Transfer_Objects.Entities;
using Data_Transfer_Objects.Requests;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace Business_Logic.Services
{
    public class IdentityService
    {
        private readonly RazorTemplateHelper razorTemplateHelper;
        private readonly ILogger<IdentityService> logger;
        private readonly UserManager<User> userManager;
        private readonly FacebookHelper facebookHelper;
        private readonly EmailService emailService;
        private readonly UserCommand userCommand;
        private readonly IJwtHelper jwtHelper;
        private readonly DataContext context;
        private readonly UserQuery userQuery;
        private readonly IMapper mapper;

        public IdentityService(
            RazorTemplateHelper razorTemplateHelper,
            ILogger<IdentityService> logger,
            FacebookHelper facebookHelper,
            UserManager<User> userManager,
            EmailService emailService,
            IJwtHelper jwtHelper,
            DataContext context,
            IMapper mapper)
        {
            this.razorTemplateHelper = razorTemplateHelper;
            this.facebookHelper = facebookHelper;
            this.emailService = emailService;
            this.userManager = userManager;
            userCommand = new (context);
            this.jwtHelper = jwtHelper;
            userQuery = new (context);
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
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

        public async Task<string> LoginAsync(LoginRequest request)
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
            
            var userDto = mapper.Map<UserDTO>(user);
            userDto.Role = string.Join(", ", await userManager.GetRolesAsync(user)); 

            logger.LogInformation($"Was login user with id {user.Id}");

            TimeSpan duration = AppEnv.AuthExpirationTime.OneDay;

            if (request.Remember)
            {
                duration = AppEnv.AuthExpirationTime.SevenDays;
            }

            return jwtHelper.GenerateToken(userDto, duration);
        }

        public UserDTO GetUser(string userId)
        {
            return mapper.Map<UserDTO>(userQuery.GetById(userId));
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

        public UserDTO ChangePersonalData(string userId, ChangeProfileRequest request)
        {
            var user = userQuery.GetById(userId);

            if (user == null)
            {
                throw new NotFoundRestException($"User with id: {userId} not found");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.BirthDay = request.BirthDay;
            
            userCommand.UpdateUser(user);

            context.SaveChanges();

            return mapper.Map<UserDTO>(user);
        }

        public async Task ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var user = await userManager.FindByIdAsync(userId);

            var result = await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                throw new BadRequestRestException("Password cannot be changed", result.Errors);
            }
        }

        public async Task<string> FacebookLogin(FacebookLoginRequest request)
        {
            if (!await facebookHelper.VerifyToken(request.Token))
            {
                throw new BadRequestRestException("Wrong access token");
            }

            var account = await facebookHelper.ReadFacebookAccountAsync(request.UserId, request.Token);

            if (string.IsNullOrEmpty(account.Email))
            {
                throw new BadRequestRestException("Facebook account, not contains email address");
            }
            
            var user = await userManager.FindByEmailAsync(account.Email);

            if (user == null)
            {
                var newUser = new User
                {
                    Email = account.Email,
                    UserName = account.Email,
                    BirthDay = account.BirthDay,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                };

                var result = await userManager.CreateAsync(newUser);

                if (!result.Succeeded)
                {
                    throw new BadRequestRestException("User was not created", result.Errors);
                }

                await userManager.AddToRoleAsync(newUser, AppEnv.Roles.Customer);

                var userDto = mapper.Map<UserDTO>(newUser);
                userDto.Role = string.Join(", ", await userManager.GetRolesAsync(newUser));

                return jwtHelper.GenerateToken(userDto, AppEnv.AuthExpirationTime.SevenDays);
            }
            else
            {
                var userDto = mapper.Map<UserDTO>(user);
                userDto.Role = string.Join(", ", await userManager.GetRolesAsync(user));
                
                return jwtHelper.GenerateToken(userDto, AppEnv.AuthExpirationTime.SevenDays);
            }
        }
    }
}