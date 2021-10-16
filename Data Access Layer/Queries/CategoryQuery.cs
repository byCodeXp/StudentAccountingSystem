using System;
using System.Linq;
using Data_Access_Layer.Models;

namespace Data_Access_Layer.Queries
{
    public class CategoryQuery
    {
        private readonly DataContext context;

        public CategoryQuery(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories;
        }

        public Category GetById(Guid id)
        {
            return context.Categories.Find(id);
        }

        public IQueryable<Category> Search(string query)
        {
            return context.Categories.OrderBy(m => m.Name).Where(m => m.Name.Contains(query));
        }

        public bool ExistsWithName(string name, params Category[] ignore)
        {
            if (ignore != null)
            {
                return context.Categories.Where(m => !ignore.Contains(m)).Any(m => m.Name.Equals(name));
            }
            return context.Categories.Any(m => m.Name.Equals(name));
        }
    }
}