using System;
using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Models
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime UpdatedTimeStamp { get; set; }
    }
}
