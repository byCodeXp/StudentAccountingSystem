using System;
using System.Linq;
using Data_Access_Layer.Models;
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
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

            builder
                .Entity<User>()
                .HasMany(c => c.SubscribedCourses)
                .WithMany(u => u.SubscribedUsers)
                .UsingEntity<Subscribe>(
                    s => s.HasOne(m => m.Course).WithMany(),
                    s => s.HasOne(m => m.User).WithMany(),
                    s =>
                    {
                        s.HasKey(m => m.Id);
                        s.Property(m => m.Jobs)
                            .HasConversion(
                                v => string.Join(',', v),
                                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                            );
                    }
                );
            
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
    }
}
