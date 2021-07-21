using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_API.Data_Access_Layer.Models;

namespace Web_API.Data_Access_Layer
{
    public class ApplicationDataContext : IdentityDbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options): base(options)
        {
            
        }

        public DbSet<Course> Courses { get; set; }
        public new DbSet<AppUser> Users { get; set; }
    }
}