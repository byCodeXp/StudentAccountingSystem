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

        public Category GetByName(string name)
        {
            return context.Categories.FirstOrDefault(m => m.Name == name);
        }

        public bool ExistsWithName(string name)
        {
            return context.Categories.Any(m => m.Name.Equals(name));
        }
    }
}