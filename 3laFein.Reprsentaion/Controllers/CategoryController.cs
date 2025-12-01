using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3laFein.Reprsentaion.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CategoryController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _serviceManager.CategoryService.GetCategoriesAsync(false);
            return Ok(categories);
        }

        [HttpGet("{categoryId:int}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await _serviceManager.CategoryService.GetCategoryAsync(categoryId, false);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory( [FromBody] CategoryForCreationDto categoryForCreationDto)
        {
            var category = await _serviceManager.CategoryService.CreateCategoryAsync(categoryForCreationDto);
            return CreatedAtRoute("GetCategoryById", new { categoryId = category.CategoryId }, category);
        }

        [HttpPut("{categoryId:int}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, CategoryForUpdateDto categoryForUpdateDto)
        {
            await _serviceManager.CategoryService.UpdateCategoryAsync(categoryId, categoryForUpdateDto, true);
            return NoContent();
        }

        [HttpDelete("{categoryId:int}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _serviceManager.CategoryService.DeleteCategoryAsync(categoryId, true);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.TryAdd("Allow", "GET, POST, PUT, DELETE, OPTIONS");
            return Ok();
        }
    }
}
