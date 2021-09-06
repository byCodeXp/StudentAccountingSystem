using System;
using System.Linq;
using Data_Access_Layer.Models;

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
            return context.Users;
        }

        public User GetOne(Guid id)
        {
            return context.Users.Find(id);
        }

        public int GetCount()
        {
            return context.Users.Count();
        }
    }
}