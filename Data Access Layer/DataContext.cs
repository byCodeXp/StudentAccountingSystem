using Data_Access_Layer.Models;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new BadgeConfiguration());

            builder.Entity<IdentityRole>().HasData
            (
                new IdentityRole(AppEnv.Roles.Admin),
                new IdentityRole(AppEnv.Roles.Customer)
            );

            // TODO: Seed admin

            base.OnModelCreating(builder);
        }

        public DbSet<Badge> Badges { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
