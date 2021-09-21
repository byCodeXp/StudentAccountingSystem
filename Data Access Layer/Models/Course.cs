using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Data_Access_Layer.Models
{
    public class Course : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Preview { get; set; }
        public int Views { get; set; }
        public List<Category> Categories { get; set; } = new();
        public List<User> SubscribedUsers { get; set; } = new();
    }

    public class CourseConfiguration : EntityConfiguration<Course>
    {
        public override void Configure(EntityTypeBuilder<Course> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.Name).HasMaxLength(128).IsRequired();
            builder.Property(m => m.Description).IsRequired();
            builder.HasMany(m => m.Categories).WithMany(m => m.Courses);
        }
    }
}
