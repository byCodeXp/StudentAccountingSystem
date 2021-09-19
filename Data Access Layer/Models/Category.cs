using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public List<Course> Courses { get; set; }
    }

    public class CategoryConfiguration : EntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.HasIndex(m => m.Name).IsUnique();
            builder.Property(m => m.Name).IsRequired();
            builder.HasMany(m => m.Courses).WithMany(m => m.Categories);
        }
    }
}
