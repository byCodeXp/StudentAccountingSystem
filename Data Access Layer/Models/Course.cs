using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data_Access_Layer.Models
{
    public class Course : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Badge> Badges { get; set; }
    }

    // TODO: Use fluent validation
    // TODO: Add relationship with users

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(m => m.Name).HasMaxLength(128).IsRequired();
            builder.Property(m => m.Description).IsRequired();
            builder.Property(m => m.CreatedTimeStamp).IsRequired();
            builder.Property(m => m.UpdatedTimeStamp).IsRequired();
            builder.HasMany(m => m.Badges).WithMany(m => m.Courses);
        }
    }
}
