using System;
using FluentValidation;

namespace Data_Transfer_Objects.Requests
{
    public class SubscribeRequest
    {
        public Guid CourseId { get; set; }
        public DateTime Date { get; set; }
    }

    public class SubscribeRequestValidation : AbstractValidator<SubscribeRequest>
    {
        public SubscribeRequestValidation()
        {
            RuleFor(m => m.CourseId).NotEmpty();
            RuleFor(m => m.Date).GreaterThan(DateTime.Today);
        }
    }
}