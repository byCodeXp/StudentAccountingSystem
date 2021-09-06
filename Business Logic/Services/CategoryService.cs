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

namespace Business_Logic.Services
{
    public class CategoryService
    {
        private readonly CategoryQuery categoryQuery;
        private readonly CategoryCommand categoryCommand;
        private readonly IMapper mapper;
        
        public CategoryService(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            categoryCommand = new(context);
            categoryQuery = new(context);
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return mapper.Map<IEnumerable<CategoryDTO>>(
                categoryQuery.GetAll().OrderBy(m => m.Name)
            );
        }

        public CategoryDTO GetCategoryById(Guid id)
        {
            var category = categoryQuery.GetById(id);

            if (category == null)
            {
                throw new HttpResponseException($"Category with id: {id}, was not found");
            }

            return mapper.Map<CategoryDTO>(category);
        }

        public void CreateCategory(CategoryDTO categoryDto)
        {
            if (categoryQuery.ExistsWithName(categoryDto.Name))
            {
                throw new HttpResponseException($"Category with name: {categoryDto.Name} already exists");
            }
            
            categoryCommand.Add(mapper.Map<Category>(categoryDto));
        }

        public void EditCategory(Guid id, CategoryDTO categoryDto)
        {
            var category = categoryQuery.GetById(id);

            if (category == null)
            {
                throw new HttpResponseException($"Category with id: {id}, was not found");
            }

            if (categoryQuery.ExistsWithName(categoryDto.Name))
            {
                throw new HttpResponseException($"Category with name: {categoryDto.Name} already exists");
            }

            category.Name = categoryDto.Name;
            category.UpdatedTimeStamp = DateTime.UtcNow;
            
            categoryCommand.Update(category);
        }

        public void RemoveCategory(Guid id)
        {
            var category = categoryQuery.GetById(id);

            if (category == null)
            {
                throw new HttpResponseException($"Category with id: {id}, was not found");
            }
            
            categoryCommand.Delete(category);
        }
    }
}