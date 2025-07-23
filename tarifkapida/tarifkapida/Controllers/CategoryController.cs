using Microsoft.AspNetCore.Mvc;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.Requests;
using tarifkapida.Services;

namespace tarifkapida.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var categories = await categoryService.GetCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("GetCategoryById/{categoryId}")]
        public async Task<ActionResult<Category?>> GetCategoryById(int categoryId)
        {
            var category = await categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost("CreateCategory")]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category data is null");
            }
            var createdCategory = await categoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { categoryId = createdCategory.CategoryId }, createdCategory);
        }
        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<Category?>> UpdateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category data is null");
            }
            var updatedCategory = await categoryService.UpdateCategoryAsync(category);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }
        [HttpPost("DeleteCategory/{categoryId}")]
        public async Task<ActionResult<bool>> DeleteCategory(int categoryId)
        {
            var isDeleted = await categoryService.DeleteCategoryAsync(categoryId);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok(isDeleted);
        }
    }
}
