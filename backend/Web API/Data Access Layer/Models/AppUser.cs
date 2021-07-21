using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Web_API.Data_Access_Layer.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}