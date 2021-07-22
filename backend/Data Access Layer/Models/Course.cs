using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Models
{
    public class Course : Entity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime FinishDate { get; set; }
        public ICollection<User> Users { get; set; }
    }
    
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            // builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(256);
            builder.Property(m => m.StartDate).IsRequired();
            builder.Property(m => m.FinishDate).IsRequired();
            builder.HasMany(m => m.Users).WithMany(m => m.Courses);
        }
    }
}