using System;
using FluentValidation;

namespace Data_Transfer_Objects.Requests
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
    }

    public class RegisterRequestValidation : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidation()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty().MinimumLength(8);
            RuleFor(m => m.BirthDay).NotEmpty().LessThan(DateTime.Today);
        }
    }
}
