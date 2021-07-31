using System.Collections.Generic;
using Data_Transfer_Objects;

namespace Business_Logic.ViewModels
{
    public class CourseVM
    {
        public int TotalCount { get; set; }
        public IEnumerable<CourseDTO> Courses { get; set; } 
    }
}