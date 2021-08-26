using FluentValidation;

namespace Data_Transfer_Objects.Requests
{
    public enum SortBy
    {
        Desc,
        Asc
    };
    
    public class GetPageRequest
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public SortBy SortBy { get; set; }

        public int Offset => Page <= 1 ? 0 : Page * PerPage - PerPage;
    }

    public class GetPageRequestValid : AbstractValidator<GetPageRequest>
    {
        public GetPageRequestValid()
        {
            RuleFor(m => m.Page).NotEmpty().GreaterThan(0);
            RuleFor(m => m.PerPage).NotEmpty().GreaterThan(0);
            RuleFor(m => m.SortBy).IsInEnum();
        }
    }
}