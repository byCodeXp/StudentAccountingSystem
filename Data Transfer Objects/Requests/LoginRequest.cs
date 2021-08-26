using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Data_Transfer_Objects.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty().MinimumLength(8);
        }
    }
}
