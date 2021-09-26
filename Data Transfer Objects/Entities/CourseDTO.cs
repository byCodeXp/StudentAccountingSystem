using System;
using System.Collections.Generic;
using FluentValidation;

namespace Data_Transfer_Objects.Entities
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Preview { get; set; }
        public int Views { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public bool Subscribed { get; set; }
    }

    public class CourseDTOValidation : AbstractValidator<CourseDTO>
    {
        public CourseDTOValidation()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Description).NotEmpty();
        }
    }
}
