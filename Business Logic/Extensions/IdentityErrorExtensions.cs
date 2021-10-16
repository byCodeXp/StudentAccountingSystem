using Microsoft.AspNetCore.Identity;

namespace Business_Logic.Extensions
{
    public static class IdentityErrorExtensions
    {
        public static string StringifyError(this IdentityError error)
        {
            return $"Code: {error.Code}, Description: {error.Description}";
        }
    }
}