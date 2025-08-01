using Microsoft.EntityFrameworkCore;
using tarifkapida.Data;
using tarifkapida.Interfaces;
using tarifkapida.Models;

namespace tarifkapida.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly AppDbContext _dbContext;

        public FavoriteService(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> AddToFavoritesAsync(int userId, int recipeId)
        {
            try
            {
                var user = await _dbContext.USER.FindAsync(userId);
                var recipe = await _dbContext.RECIPE.FindAsync(recipeId);
                if (user == null || recipe == null)
                    return false;

                // Favori zaten var mı kontrolü
                var alreadyFavorite = await _dbContext.FAVORITE.AnyAsync(f => f.UserId == userId && f.RecipeId == recipeId);
                if (alreadyFavorite)
                    return false;

                // Favori ekle
                var favorite = new Favorite { UserId = userId, RecipeId = recipeId, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                _dbContext.FAVORITE.Add(favorite);
                recipe.FavoriteCount = await _dbContext.FAVORITE.CountAsync(f => f.RecipeId == recipeId) + 1;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveFromFavoritesAsync(int userId, int recipeId)
        {
            try
            {
                var user = await _dbContext.USER.FindAsync(userId);
                var recipe = await _dbContext.RECIPE.FindAsync(recipeId);
                if (user == null || recipe == null)
                    return false;

                var favorite = await _dbContext.FAVORITE.FirstOrDefaultAsync(f => f.UserId == userId && f.RecipeId == recipeId);
                if (favorite == null)
                    return false;

                _dbContext.FAVORITE.Remove(favorite);
                recipe.FavoriteCount = await _dbContext.FAVORITE.CountAsync(f => f.RecipeId == recipeId) - 1;
                if (recipe.FavoriteCount < 0) recipe.FavoriteCount = 0;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Recipe>> GetUserFavoritesAsync(int userId)
        {
            try
            {
                var favoriteRecipeIds = await _dbContext.FAVORITE
                    .Where(f => f.UserId == userId)
                    .Select(f => f.RecipeId)
                    .ToListAsync();

                var favoriteRecipes = await _dbContext.RECIPE
                    .Where(r => favoriteRecipeIds.Contains(r.RecipeId))
                    .Include(r => r.User)
                    .Include(r => r.Reviews)
                    .ToListAsync();

                return favoriteRecipes;
            }
            catch
            {
                return new List<Recipe>();
            }
        }

        public async Task<List<Recipe>> SearchUserFavoritesAsync(int userId, string searchTerm)
        {
            try
            {
                var favoriteRecipeIds = await _dbContext.FAVORITE
                    .Where(f => f.UserId == userId)
                    .Select(f => f.RecipeId)
                    .ToListAsync();

                var filteredRecipes = await _dbContext.RECIPE
                    .Where(r => favoriteRecipeIds.Contains(r.RecipeId) &&
                        (r.RecipeName.Contains(searchTerm) || r.RecipeDescription.Contains(searchTerm)))
                    .Include(r => r.User)
                    .Include(r => r.Reviews)
                    .ToListAsync();

                return filteredRecipes;
            }
            catch
            {
                return new List<Recipe>();
            }
        }

        public async Task<bool> IsFavoriteAsync(int userId, int recipeId)
        {
            try
            {
                return await _dbContext.FAVORITE.AnyAsync(f => f.UserId == userId && f.RecipeId == recipeId);
            }
            catch
            {
                return false;
            }
        }

    }
}