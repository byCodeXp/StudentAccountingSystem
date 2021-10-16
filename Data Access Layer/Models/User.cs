using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_Access_Layer.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime UpdatedTimeStamp { get; set; }
        public List<Course> Courses { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(m => m.Email).IsRequired().HasMaxLength(128);
            builder.HasIndex(m => m.Email).IsUnique();
            builder.Property(m => m.FirstName).HasMaxLength(128);
            builder.Property(m => m.LastName).HasMaxLength(128);
            builder.Property(m => m.CreatedTimeStamp).HasDefaultValue(DateTime.UtcNow).ValueGeneratedOnAdd();
            builder.Property(m => m.UpdatedTimeStamp).HasDefaultValue(DateTime.UtcNow).ValueGeneratedOnAddOrUpdate();

            builder
                .HasMany(c => c.Courses)
                .WithMany(c => c.Users)
                .UsingEntity<UserCourse>(
                    r => r.HasOne(m => m.Course).WithMany().HasForeignKey(m => m.CourseId),
                    r => r.HasOne(m => m.User).WithMany().HasForeignKey(m => m.UserId),
                    r =>
                    {
                        r.HasKey(m => new { m.UserId, m.CourseId });
                        r.Property(m => m.Jobs)
                            .HasConversion(
                                v => string.Join(',', v),
                                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                            );
                    }
                );
        }
    }
}
