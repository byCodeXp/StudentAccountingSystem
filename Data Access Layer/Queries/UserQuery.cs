using System;
using System.Collections.Generic;
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
            return context.Users.Include(m => m.SubscribedCourses);
        }

        public User GetById(Guid id)
        {
            return context.Users.Find(id);
        }

        public int GetCount()
        {
            return context.Users.Count();
        }

        public IEnumerable<Course> GetCoursesByUser (User user)
        {
            return context.Users.Include(m => m.SubscribedCourses).FirstOrDefault(m => m.Id == user.Id)?.SubscribedCourses;
        }
        
    }
}