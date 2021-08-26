using System;
using System.Linq;
using Data_Access_Layer.Models;

namespace Data_Access_Layer.Queries
{
    public class CategoryQuery
    {
        private readonly DataContext _context;

        public CategoryQuery(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category GetOne(Guid id)
        {
            return _context.Categories.Find(id);
        }

        public bool ExistsWithName(string name)
        {
            return _context.Categories.Any(m => m.Name.Equals(name));
        }
    }
}