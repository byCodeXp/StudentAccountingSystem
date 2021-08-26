using System;
using Business_Logic.Services;
using Data_Transfer_Objects;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryService.GetCategories());
        }

        [HttpPost("create")]
        public IActionResult Create(CategoryDTO category)
        {
            _categoryService.CreateCategory(category);
            return Ok();
        }
        
        [HttpPut("update")]
        public IActionResult Update(Guid id, CategoryDTO category)
        {
            _categoryService.EditCategory(id, category);
            return Ok();
        }
        
        [HttpDelete("delete")]
        public IActionResult Delete(Guid id)
        {
            _categoryService.RemoveCategory(id);
            return Ok();
        }
    }
}