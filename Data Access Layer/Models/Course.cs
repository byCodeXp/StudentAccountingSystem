using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;

namespace Data_Access_Layer.Models
{
    public class Course : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Preview { get; set; }
        public int Views { get; set; }
        public List<Category> Categories { get; set; }
        public List<User> Users { get; set; }
    }

    public class CourseConfiguration : EntityConfiguration<Course>
    {
        public override void Configure(EntityTypeBuilder<Course> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.Name).HasMaxLength(128).IsRequired();
            builder.Property(m => m.Description).IsRequired();
            builder.HasMany(m => m.Categories).WithMany(m => m.Courses);
            
            builder
                .HasMany(c => c.Users)
                .WithMany(c => c.Courses)
                .UsingEntity<UserCourse>(
                    r => r.HasOne(m => m.User).WithMany().HasForeignKey(m => m.UserId),
                    r => r.HasOne(m => m.Course).WithMany().HasForeignKey(m => m.CourseId),
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
