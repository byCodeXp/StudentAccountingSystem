using System;
using FluentValidation;

namespace Data_Transfer_Objects.Entities
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }

    public class CategoryDTOValidation : AbstractValidator<CategoryDTO>
    {
        public CategoryDTOValidation()
        {
            RuleFor(m => m.Name).NotEmpty();
        }
    }
}