using System.Collections.Generic;
using Data_Transfer_Objects.Entities;

namespace Data_Transfer_Objects.ViewModels
{
    public class CourseVM
    {
        public int TotalCount { get; set; }
        public List<CourseDTO> Courses { get; set; }
    }
}