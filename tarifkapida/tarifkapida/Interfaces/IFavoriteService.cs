using tarifkapida.Models;

namespace tarifkapida.Interfaces
{
    public interface IFavoriteService
    {
        Task<bool> AddToFavoritesAsync(int userId, int recipeId);
        Task<bool> RemoveFromFavoritesAsync(int userId, int recipeId);
        Task<List<Recipe>> GetUserFavoritesAsync(int userId);
        Task<bool> IsFavoriteAsync(int userId, int recipeId);
        Task<List<Recipe>> SearchUserFavoritesAsync(int userId, string searchTerm);
    }
} 