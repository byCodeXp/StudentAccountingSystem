using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Data_Access_Layer;
using Data_Access_Layer.Commands;
using Data_Access_Layer.Models;
using Data_Access_Layer.Queries;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Errors;

namespace Business_Logic.Services
{
    public class CategoryService
    {
        private readonly CategoryQuery _categoryQuery;
        private readonly CategoryCommand _categoryCommand;
        private readonly IMapper _mapper;
        
        public CategoryService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _categoryCommand = new(context);
            _categoryQuery = new(context);
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(
                _categoryQuery.GetAll().OrderBy(m => m.Name)
            );
        }

        public CategoryDTO GetCategoryById(Guid id)
        {
            var category = _categoryQuery.GetOne(id);

            if (category == null)
            {
                throw new HttpResponseException($"Category with id: {id}, was not found");
            }

            return _mapper.Map<CategoryDTO>(category);
        }

        public void CreateCategory(CategoryDTO categoryDto)
        {
            if (_categoryQuery.ExistsWithName(categoryDto.Name))
            {
                throw new HttpResponseException($"Category with name: {categoryDto.Name} already exists");
            }
            
            _categoryCommand.Add(_mapper.Map<Category>(categoryDto));
        }

        public void EditCategory(Guid id, CategoryDTO categoryDto)
        {
            var category = _categoryQuery.GetOne(id);

            if (category == null)
            {
                throw new HttpResponseException($"Category with id: {id}, was not found");
            }

            if (_categoryQuery.ExistsWithName(categoryDto.Name))
            {
                throw new HttpResponseException($"Category with name: {categoryDto.Name} already exists");
            }

            category.Name = categoryDto.Name;
            category.UpdatedTimeStamp = DateTime.UtcNow;
            
            _categoryCommand.Update(category);
        }

        public void RemoveCategory(Guid id)
        {
            var category = _categoryQuery.GetOne(id);

            if (category == null)
            {
                throw new HttpResponseException($"Category with id: {id}, was not found");
            }
            
            _categoryCommand.Delete(category);
        }
    }
}