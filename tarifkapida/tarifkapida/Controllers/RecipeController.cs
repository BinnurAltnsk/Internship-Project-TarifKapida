using Microsoft.AspNetCore.Mvc;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.Requests;
using tarifkapida.Services;

namespace tarifkapida.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RecipeController : ControllerBase
    {
        readonly IRecipeService recipeService;
        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpGet("GetRecipes")]
        public async Task<ActionResult<List<RecipeWithReviewsDto>>> GetRecipes()
        {
            var recipes = await recipeService.GetRecipesAsync();
            return Ok(recipes);
        }

        [HttpGet("GetRecipesTest")]
        public async Task<ActionResult<List<Recipe>>> GetRecipesTest()
        {
            var recipes = await recipeService.GetRecipesTestAsync();
            return Ok(recipes);
        }

        [HttpGet("GetRecipeById/{recipeId}")]
        public async Task<ActionResult<Recipe?>> GetRecipeById(int recipeId)
        {
            var recipe = await recipeService.GetRecipeByIdAsync(recipeId);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }
        
        [HttpPost("CreateRecipe")]
        public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null)
            {
                return BadRequest("Recipe data is null");
            }
            var createdRecipe = await recipeService.CreateRecipeAsync(recipe);
            return CreatedAtAction(nameof(GetRecipeById), new { recipeId = createdRecipe.RecipeId }, createdRecipe);
        }

        [HttpPut("UpdateRecipe")]
        public async Task<ActionResult<Recipe?>> UpdateRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null)
            {
                return BadRequest("Recipe data is null");
            }
            var updatedRecipe = await recipeService.UpdateRecipeAsync(recipe);
            if (updatedRecipe == null)
            {
                return NotFound();
            }
            return Ok(updatedRecipe);
        }

        [HttpPost("DeleteRecipe/{recipeId}")]
        public async Task<IActionResult> DeleteRecipe([FromQuery] int recipeId)
        {
            var result = await recipeService.DeleteRecipeAsync(recipeId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("SearchRecipes")]
        public async Task<ActionResult<List<Recipe>>> SearchRecipes(string searchTerm)
        {
            var recipes = await recipeService.SearchRecipesAsync(searchTerm);
            return Ok(recipes);
        }

        [HttpGet("GetRecipesByUserId/{userId}")]
        public async Task<ActionResult<List<Recipe>>> GetRecipesByUserId(int userId)
        {
            var recipes = await recipeService.GetRecipesByUserIdAsync(userId);
            return Ok(recipes);
        }
    }
}
