using System.Collections.Generic;
using Data_Transfer_Objects;

namespace API.ViewModels
{
    public class CourseVM
    {
        public int TotalCount { get; set; }
        public IEnumerable<CourseDTO> Courses { get; set; } 
    }
}