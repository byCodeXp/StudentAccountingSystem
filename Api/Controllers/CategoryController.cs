using System;
using Business_Logic.Services;
using Data_Transfer_Objects;
using Data_Transfer_Objects.Entities;
using Data_Transfer_Objects.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService;
        
        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] CategoriesRequest request)
        {
            return Ok(categoryService.GetCategories(request));
        }

        [HttpPost("create")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Create(CategoryDTO category)
        {
            categoryService.CreateCategory(category);
            return Ok();
        }
        
        [HttpPut("update")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Update(CategoryDTO category)
        {
            return Ok(categoryService.UpdateCategory(category));
        }
        
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = AppEnv.Roles.Admin)]
        public IActionResult Delete(Guid id)
        {
            categoryService.RemoveCategory(id);
            return Ok();
        }
    }
}