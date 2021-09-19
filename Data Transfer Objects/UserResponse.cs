using System.Collections.Generic;
using Data_Transfer_Objects.Entities;

namespace Data_Transfer_Objects
{
    public class UserResponse
    {
        public string Token { get; set; }
        public IEnumerable<CourseDTO> Courses { get; set; }
    }
}