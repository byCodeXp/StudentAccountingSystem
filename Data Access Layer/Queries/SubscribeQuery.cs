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
            return context.Subscribes.Where(c => c.User == user).Select(m => m.Course);
        }
    }
}