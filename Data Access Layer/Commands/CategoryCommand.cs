using Data_Access_Layer.Models;

namespace Data_Access_Layer.Commands
{
    public class CategoryCommand
    {
        private readonly DataContext context;

        public CategoryCommand(DataContext context)
        {
            this.context = context;
        }

        public void Add(Category category)
        {
            context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            context.Categories.Remove(category);
        }
    }
}