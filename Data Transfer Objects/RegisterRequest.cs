using System.ComponentModel.DataAnnotations;

namespace Data_Transfer_Objects
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int Age { get; set; }
    }
}
