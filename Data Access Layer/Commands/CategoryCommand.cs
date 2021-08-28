using Data_Access_Layer.Models;

namespace Data_Access_Layer.Commands
{
    public class CategoryCommand
    {
        private readonly DataContext _context;

        public CategoryCommand(DataContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}