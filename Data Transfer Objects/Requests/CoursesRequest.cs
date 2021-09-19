using System.Collections.Generic;
using FluentValidation;

namespace Data_Transfer_Objects.Requests
{
    public enum SortBy
    {
        Relevance,
        New,
        Popular,
        Alphabetically
    };
    
    public class CoursesRequest
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string SortBy { get; set; }
        public List<string> Categories { get; set; }
    }

    public class GetPageRequestValid : AbstractValidator<CoursesRequest>
    {
        public GetPageRequestValid()
        {
            RuleFor(m => m.Page).NotEmpty().GreaterThan(0);
            RuleFor(m => m.PerPage).NotEmpty().GreaterThan(0);
        }
    }
}