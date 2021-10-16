using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Business_Logic.Extensions
{
    public static class UserHttpExtensions
    {
        public static string GetUserId(this HttpContext context)
        {
            return context.User.Claims.FirstOrDefault(m => m.Type == "id")?.Value;
        }
    }
}