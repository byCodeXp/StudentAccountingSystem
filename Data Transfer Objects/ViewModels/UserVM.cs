using Data_Transfer_Objects.Entities;

namespace Data_Transfer_Objects.ViewModels
{
    public class UserVM
    {
        public int TotalCount { get; set; }
        public UserDTO[] Users { get; set; }
    }
}