using System;
using System.Collections.Generic;

namespace Web_API.Data_Access_Layer.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public IEnumerable<AppUser> Users { get; set; }
    }
}