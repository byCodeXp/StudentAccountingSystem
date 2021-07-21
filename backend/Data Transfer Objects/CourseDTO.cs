using System;
using System.Collections.Generic;

namespace Data_Transfer_Objects
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime FinishDate { get; set; }
        public ICollection<UserDTO> Users { get; set; }
    }
}