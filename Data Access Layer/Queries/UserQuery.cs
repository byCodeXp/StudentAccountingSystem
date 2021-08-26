using System;
using System.Linq;
using Data_Access_Layer.Models;

namespace Data_Access_Layer.Queries
{
    public class UserQuery
    {
        private readonly DataContext _context;

        public UserQuery(DataContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetOne(Guid id)
        {
            return _context.Users.Find(id);
        }

        public int GetCount()
        {
            return _context.Users.Count();
        }
    }
}