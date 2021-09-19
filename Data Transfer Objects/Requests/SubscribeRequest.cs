using System;

namespace Data_Transfer_Objects.Requests
{
    public class SubscribeRequest
    {
        public Guid CourseId { get; set; }
        public DateTime Date { get; set; }
    }
}