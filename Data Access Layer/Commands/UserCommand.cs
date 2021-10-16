using System.Collections.Generic;
using System.Linq;
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

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
        }

        public void SubscribeCourse(User user, Course course, List<string> jobs)
        {
            context.UsersCourses.Add(new UserCourse { User = user, Course = course, Jobs = jobs });
        }

        public List<string> UnsubscribeCourse(User user, Course course)
        {
            var userCourse = context.UsersCourses.First(m => m.User == user && m.Course == course);
            var jobs = userCourse.Jobs;
            context.UsersCourses.Remove(userCourse);
            return jobs;
        }
    }
}