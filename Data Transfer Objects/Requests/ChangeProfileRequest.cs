using System;

namespace Data_Transfer_Objects.Requests
{
    public class ChangeProfileRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}