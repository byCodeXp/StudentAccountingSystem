using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime UpdatedTimeStamp { get; set; }

        public ICollection<Course> SubscribedCourses { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(m => m.Email).IsRequired().HasMaxLength(128);
            builder.HasIndex(m => m.Email).IsUnique();
            builder.Property(m => m.FirstName).HasMaxLength(128);
            builder.Property(m => m.LastName).HasMaxLength(128);
            builder.Property(m => m.CreatedTimeStamp).IsRequired();
            builder.Property(m => m.UpdatedTimeStamp).IsRequired();
            builder.HasMany(m => m.SubscribedCourses).WithMany(m => m.SubscribedUsers);

        }
    }
}
