namespace Data_Transfer_Objects.Requests
{
    public class UsersRequest
    {
        public string Search { get; set; }
        public string SortBy { get; set; }
        public bool Ascending { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
    }
}