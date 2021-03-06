using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Models
{
    public class UserCourse
    {
        public string UserId { get; set; }
        public Guid CourseId { get; set; }
        public User User { get; set; }
        public Course Course { get; set; }
        public List<string> Jobs { get; set; }
    }
}