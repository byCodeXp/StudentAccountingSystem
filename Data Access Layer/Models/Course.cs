using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data_Access_Layer.Models
{
    public class Course : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Preview { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<User> SubscribedUsers { get; set; }
    }

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(m => m.Name).HasMaxLength(128).IsRequired();
            builder.Property(m => m.Description).IsRequired();
            builder.Property(m => m.CreatedTimeStamp).IsRequired();
            builder.Property(m => m.UpdatedTimeStamp).IsRequired();
            builder.HasMany(m => m.Categories).WithMany(m => m.Courses);
            builder.HasMany(m => m.SubscribedUsers).WithMany(m => m.SubscribedCourses);
        }
    }
}
