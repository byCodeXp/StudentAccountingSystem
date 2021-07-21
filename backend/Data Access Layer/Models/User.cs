using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Course> Courses { get; set; }
    }

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(m => m.FirstName).HasMaxLength(128);
            builder.Property(m => m.LastName).HasMaxLength(128);
            builder.HasMany(m => m.Courses).WithMany(m => m.Users);
        }
    }
}