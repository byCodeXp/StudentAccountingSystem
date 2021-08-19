using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data_Access_Layer.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }

    public class BadgeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(m => m.Name).IsUnique();
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.CreatedTimeStamp).IsRequired();
            builder.Property(m => m.UpdatedTimeStamp).IsRequired();
            builder.HasMany(m => m.Courses).WithMany(m => m.Categories);
        }
    }
}
