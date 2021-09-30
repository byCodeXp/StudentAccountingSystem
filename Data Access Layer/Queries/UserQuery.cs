using System.Linq;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Queries
{
    public class UserQuery
    {
        private readonly DataContext context;

        public UserQuery(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<User> GetAll()
        {
            return context.Users.Include(m => m.Courses);
        }

        public User GetById(string id)
        {
            return context.Users.Find(id);
        }

        public bool UserExistsCourse(User user, Course course)
        {
            return context.UsersCourses.Any(m => m.User == user && m.Course == course);
        }

        public IQueryable<Course> UserCourses(User user)
        {
            return context.UsersCourses.Where(m => m.User.Id == user.Id).Select(m => m.Course);
        }
    }
}