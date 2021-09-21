using Data_Access_Layer.Models;

namespace Data_Access_Layer.Commands
{
    public class UserCommand
    {
        private readonly DataContext context;

        public UserCommand(DataContext context)
        {
            this.context = context;
        }

        public void UnsubscribeCourse(User user, Course course)
        {
            user.SubscribedCourses.Remove(course);
        }
    }
}