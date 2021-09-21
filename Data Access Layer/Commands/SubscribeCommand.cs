using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer.Models;

namespace Data_Access_Layer.Commands
{
    public class SubscribeCommand
    {
        private readonly DataContext context;

        public SubscribeCommand(DataContext context)
        {
            this.context = context;
        }

        public void Subscribe(User user, Course course, List<string> jobs)
        {
            var subscribe = new Subscribe
            {
                User = user,
                Course = course,
                Jobs = jobs
            };

            context.Subscribes.Add(subscribe);
        }

        public Subscribe Unsubscribe(User user, Course course)
        {
            var subscribe = context.Subscribes.FirstOrDefault(m => m.User.Id == user.Id && m.Course.Id == course.Id);
            if (subscribe != null)
            {
                context.Subscribes.Remove(subscribe);
            }
            return subscribe;
        }
    }
}