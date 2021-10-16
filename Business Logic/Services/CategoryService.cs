using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Business_Logic.Exceptions;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Models;
using Data_Access_Layer.Queries;
using Data_Transfer_Objects.Entities;
using Data_Transfer_Objects.Requests;
using Microsoft.Extensions.Logging;

namespace Business_Logic.Services
{
    public class CategoryService
    {
        private readonly CategoryQuery categoryQuery;
        private readonly CategoryCommand categoryCommand;
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CategoryService> logger;

        public CategoryService(DataContext context, IMapper mapper, ILogger<CategoryService> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
            categoryCommand = new(context);
            categoryQuery = new(context);
        }

        public IEnumerable<CategoryDTO> GetCategories(CategoriesRequest request)
        {
            var categories = categoryQuery.GetAll();

            if (!string.IsNullOrEmpty(request.Search))
            {
                categories = categoryQuery.Search(request.Search);
            }

            logger.LogInformation($"Returned {categories.Count()} categories");

            return mapper.Map<List<CategoryDTO>>(categories);
        }

        public CategoryDTO CreateCategory(CategoryDTO categoryDto)
        {
            var category = mapper.Map<Category>(categoryDto);
            
            if (categoryQuery.ExistsWithName(category.Name))
            {
                throw new BadRequestRestException($"Category with name: {category.Name} already exists");
            }
            
            categoryCommand.Add(category);
            context.SaveChanges();
            
            logger.LogInformation($"Created category with name \"{category.Name}\"");
            
            return mapper.Map<CategoryDTO>(category);
        }

        public CategoryDTO UpdateCategory(CategoryDTO categoryDto)
        {
            var category = categoryQuery.GetById(categoryDto.Id);

            if (category == null)
            {
                throw new NotFoundRestException($"Category with id: {categoryDto.Id}, was not found");
            }

            if (categoryQuery.ExistsWithName(categoryDto.Name, category))
            {
                throw new BadRequestRestException($"Category with name: {categoryDto.Name} already exists");
            }

            category.Name = categoryDto.Name;
            category.Color = categoryDto.Color;
            
            categoryCommand.Update(category);
            context.SaveChanges();
            
            logger.LogInformation($"Updated category with name \"{category.Name}\"");

            return categoryDto;
        }

        public void RemoveCategory(Guid id)
        {
            var category = categoryQuery.GetById(id);

            if (category == null)
            {
                throw new NotFoundRestException($"Category with id: {id}, was not found");
            }
            
            categoryCommand.Delete(category);
            context.SaveChanges();
            
            logger.LogInformation($"Removed category with name \"{category.Name}\"");
        }
    }
}