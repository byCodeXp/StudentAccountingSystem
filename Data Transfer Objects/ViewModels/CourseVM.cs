namespace Data_Transfer_Objects.ViewModels
{
    public class CourseVM
    {
        public int TotalCount { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public CourseDTO[] Courses { get; set; }
    }
}