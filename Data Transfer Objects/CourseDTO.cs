using System;

namespace Data_Transfer_Objects
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Preview { get; set; }
    }
}
