using System.Collections.Generic;

namespace Data_Transfer_Objects
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<CourseDTO> Courses { get; set; }
    }
}