using System.Collections.Generic;
using FluentValidation;

namespace Data_Transfer_Objects.Entities
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        
        public IEnumerable<CourseDTO> Courses { get; set; }
        public string Role { get; set;}
        
    }
    
    public class UserDTOValidation : AbstractValidator<UserDTO>
    {
        public UserDTOValidation()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Age).GreaterThan(0);
        }
    }
}
