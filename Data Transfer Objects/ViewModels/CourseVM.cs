using Data_Transfer_Objects.Entities;

namespace Data_Transfer_Objects.ViewModels
{
    public class CourseVM
    {
        public int TotalCount { get; set; }
        public CourseDTO[] Courses { get; set; }
    }
}