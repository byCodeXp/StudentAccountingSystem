using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data_Access_Layer.Models
{
    public class Badge : Entity
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }

    // TODO: Use fluent validation

    public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
    {
        public void Configure(EntityTypeBuilder<Badge> builder)
        {
            builder.HasIndex(m => m.Name).IsUnique();
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.CreatedTimeStamp).IsRequired();
            builder.Property(m => m.UpdatedTimeStamp).IsRequired();
            builder.HasMany(m => m.Courses).WithMany(m => m.Badges);
        }
    }
}
