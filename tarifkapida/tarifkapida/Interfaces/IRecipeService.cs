using tarifkapida.Models;
using tarifkapida.Models.Requests;

namespace tarifkapida.Interfaces
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int recipeId);
        Task<Recipe> CreateRecipeAsync(Recipe recipe);
        Task<Recipe?> UpdateRecipeAsync(Recipe recipe);
        Task<bool> DeleteRecipeAsync(int recipeId);
        Task<List<Recipe>> SearchRecipesAsync(string searchTerm);
        Task<List<Recipe>> GetRecipesByUserIdAsync(int userId);
    }
}
