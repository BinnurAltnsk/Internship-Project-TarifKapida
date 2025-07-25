using tarifkapida.Data;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace tarifkapida.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly AppDbContext _dbContext;

        public RecipeService(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<RecipeWithReviewsDto>> GetRecipesAsync()
        {
            var recipes = await _dbContext.RECIPE
                .Include(r => r.Reviews)
                .ThenInclude(rv =>rv.User)
                .Include(r => r.Category) // Kategori bilgisini de dahil et
                .ToListAsync();

            return recipes.Select(recipe => new RecipeWithReviewsDto
            {
                RecipeId = recipe.RecipeId,
                RecipeName = recipe.RecipeName,
                RecipeDescription = recipe.RecipeDescription,
                RecipeIngredients = recipe.RecipeIngredients,
                RecipeInstructions = recipe.RecipeInstructions,
                RecipeImageUrl = ConvertImagePathToUrl(recipe.RecipeImageUrl),
                RecipeCreatedAt = recipe.RecipeCreatedAt,
                RecipeUpdatedAt = recipe.RecipeUpdatedAt,
                CategoryName = recipe.Category?.CategoryName ?? "Kategorisiz", // Kategori adını ekle
                CategoryId = recipe.CategoryId,
                Reviews = recipe.Reviews?.Select(review => new ReviewDto
                {
                    ReviewId = review.ReviewId,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating,
                    UserId = review.UserId,
                    Username = review.User?.Username ?? "Bilinmeyen",
                    ReviewCreatedAt = review.ReviewCreatedAt,
                    ReviewUpdatedAt = review.ReviewUpdatedAt
                }).ToList() ?? new List<ReviewDto>()
            }).ToList();
        }

        public async Task<List<Recipe>> GetRecipesTestAsync()
        {
            var recipes = await _dbContext.RECIPE
                .Include(rv => rv.User)
                .Include(r => r.Reviews)
                .ThenInclude(rv => rv.User)
                .ToListAsync();

            // Resim URL'lerini dönüştür
            foreach (var recipe in recipes)
            {
                recipe.RecipeImageUrl = ConvertImagePathToUrl(recipe.RecipeImageUrl);
            }

            return recipes;
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int recipeId)
        {
            var recipe = await _dbContext.RECIPE
                .Include(r => r.Reviews)
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId);

            if (recipe != null)
            {
                recipe.RecipeImageUrl = ConvertImagePathToUrl(recipe.RecipeImageUrl);
            }

            return recipe;
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            _dbContext.RECIPE.Add(recipe);
            await _dbContext.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe?> UpdateRecipeAsync(Recipe recipe)
        {
            var existingRecipe = await _dbContext.RECIPE.FindAsync(recipe.RecipeId);
            if (existingRecipe == null)
                return null;

            existingRecipe.RecipeName = recipe.RecipeName;
            existingRecipe.RecipeDescription = recipe.RecipeDescription;
            existingRecipe.RecipeIngredients = recipe.RecipeIngredients;
            existingRecipe.RecipeInstructions = recipe.RecipeInstructions;
            existingRecipe.RecipeImageUrl = recipe.RecipeImageUrl;
            existingRecipe.UserId = recipe.UserId;
            existingRecipe.RecipeUpdatedAt = DateTime.Now;

            _dbContext.RECIPE.Update(existingRecipe);
            await _dbContext.SaveChangesAsync();
            return existingRecipe;
        }

        public async Task<bool> DeleteRecipeAsync(int recipeId)
        {
            var recipe = await _dbContext.RECIPE.FindAsync(recipeId);
            if (recipe == null)
                return false;

            _dbContext.RECIPE.Remove(recipe);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Recipe>> SearchRecipesAsync(string searchTerm)
        {
            var recipes = await _dbContext.RECIPE
                .Include(r => r.Reviews)
                .Where(r => r.RecipeName.Contains(searchTerm) || r.RecipeIngredients.Contains(searchTerm))
                .ToListAsync();

            // Resim URL'lerini dönüştür
            foreach (var recipe in recipes)
            {
                recipe.RecipeImageUrl = ConvertImagePathToUrl(recipe.RecipeImageUrl);
            }

            return recipes;
        }

        public async Task<List<Recipe>> GetRecipesByUserIdAsync(int userId)
        {
            var recipes = await _dbContext.RECIPE
                .Include(r => r.Reviews)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            // Resim URL'lerini dönüştür
            foreach (var recipe in recipes)
            {
                recipe.RecipeImageUrl = ConvertImagePathToUrl(recipe.RecipeImageUrl);
            }

            return recipes;
        }

        public async Task<bool> UpdateRecipeImageAsync(int recipeId, string imageUrl)
        {
            var recipe = await _dbContext.RECIPE.FindAsync(recipeId);
            if (recipe == null)
                return false;

            recipe.RecipeImageUrl = imageUrl;
            recipe.RecipeUpdatedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Recipe>> GetRecipesByCategoryIdAsync(int categoryId)
        {
            var recipes = await _dbContext.RECIPE
                .Where(r => r.CategoryId == categoryId)
                .Include(r => r.Reviews)
                .ToListAsync();

            // Resim URL'lerini dönüştür
            foreach (var recipe in recipes)
            {
                recipe.RecipeImageUrl = ConvertImagePathToUrl(recipe.RecipeImageUrl);
            }

            return recipes;
        }

        public async Task<List<Recipe>> GetRecipesByCategoryNameAsync(string categoryName)
        {
            var recipes = await _dbContext.RECIPE
                .Where(r => r.Category.CategoryName == categoryName)
                .Include(r => r.Reviews)
                .ToListAsync();

            // Resim URL'lerini dönüştür
            foreach (var recipe in recipes)
            {
                recipe.RecipeImageUrl = ConvertImagePathToUrl(recipe.RecipeImageUrl);
            }

            return recipes;
        }

        private string ConvertImagePathToUrl(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return string.Empty;

            // Eğer zaten web URL formatındaysa, olduğu gibi döndür
            if (imagePath.StartsWith("http://") || imagePath.StartsWith("https://"))
                return imagePath;

            // Eğer zaten /images/ ile başlıyorsa, olduğu gibi döndür
            if (imagePath.StartsWith("/images/"))
                return imagePath;

            // Tam dosya yolundan dosya adını çıkar
            var fileName = Path.GetFileName(imagePath);
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            // Web URL formatına dönüştür
            return $"/images/{fileName}";
        }
    }
}
