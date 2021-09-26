using System.Linq;
using Data_Access_Layer.Models;

namespace Data_Access_Layer.Queries
{
    public class SubscribeQuery
    {
        private readonly DataContext context;

        public SubscribeQuery(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<Course> GetUserCourses(User user)
        {
            return context.UserSubscribes.Where(c => c.User == user).Select(m => m.Course);
        }

        public bool IsUserSubscribeOnCourse(User user, Course course)
        {
            return context.UserSubscribes.Any(m => m.User == user && m.Course == course);
        }
    }
}