using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime UpdatedTimeStamp { get; set; }
    }
    
    public class EntityConfiguration<TEntity>
        : IEntityTypeConfiguration<TEntity> where TEntity
        : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.CreatedTimeStamp).HasDefaultValue(DateTime.UtcNow).ValueGeneratedOnAdd();
            builder.Property(m => m.UpdatedTimeStamp).HasDefaultValue(DateTime.UtcNow).ValueGeneratedOnAddOrUpdate();
        }
    }
}
