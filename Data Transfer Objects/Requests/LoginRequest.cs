using System.ComponentModel.DataAnnotations;

namespace Data_Transfer_Objects.Requests
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
