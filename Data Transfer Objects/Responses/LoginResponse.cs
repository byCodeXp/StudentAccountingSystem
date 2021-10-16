using Data_Transfer_Objects.Entities;

namespace Data_Transfer_Objects.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserDTO User { get; set; }
    }
}