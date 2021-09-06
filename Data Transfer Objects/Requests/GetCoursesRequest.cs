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
    
    public class GetCoursesRequest
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public SortBy SortBy { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }

    public class GetPageRequestValid : AbstractValidator<GetCoursesRequest>
    {
        public GetPageRequestValid()
        {
            RuleFor(m => m.Page).NotEmpty().GreaterThan(0);
            RuleFor(m => m.PerPage).NotEmpty().GreaterThan(0);
            RuleFor(m => m.SortBy).IsInEnum();
        }
    }
}