using tarifkapida.Models;
using tarifkapida.Models.Requests;

namespace tarifkapida.Interfaces
{
    public interface IRecipeService
    {
        Task<List<RecipeWithReviewsDto>> GetRecipesAsync();
        Task<List<Recipe>> GetRecipesTestAsync();
        Task<Recipe?> GetRecipeByIdAsync(int recipeId);
        Task<Recipe> CreateRecipeAsync(Recipe recipe);
        Task<Recipe?> UpdateRecipeAsync(Recipe recipe);
        Task<bool> DeleteRecipeAsync(int recipeId);
        Task<List<Recipe>> SearchRecipesAsync(string searchTerm);
        Task<List<Recipe>> GetRecipesByUserIdAsync(int userId);
        Task<bool> UpdateRecipeImageAsync(int recipeId, string imageUrl);
        Task<List<Recipe>> GetRecipesByCategoryIdAsync(int categoryId);
        Task<List<Recipe>> GetRecipesByCategoryNameAsync(string categoryName);
    }
}
