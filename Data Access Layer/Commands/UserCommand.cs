using System.Linq;
using Data_Access_Layer.Models;

namespace Data_Access_Layer.Commands
{
    public class UserCommand
    {
        private readonly DataContext _context;

        public UserCommand(DataContext context)
        {
            _context = context;
        }

        public bool SubscribeCourse(User user, Course course)
        {
            if (user.SubscribedCourses.Any(m => m.Id == course.Id))
            {
                return false;
            }

            user.SubscribedCourses.Add(course);
            _context.SaveChanges();
            
            return true;
        }
    }
}