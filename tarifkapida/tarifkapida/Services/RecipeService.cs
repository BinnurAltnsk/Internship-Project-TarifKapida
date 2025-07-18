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
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _dbContext.RECIPE.ToListAsync();
        }
        public async Task<Recipe?> GetRecipeByIdAsync(int recipeId)
        {
            return await _dbContext.RECIPE.FindAsync(recipeId);
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
            return await _dbContext.RECIPE
                .Where(r => r.RecipeName.Contains(searchTerm) || r.RecipeIngredients.Contains(searchTerm))
                .ToListAsync();
        }
        public async Task<List<Recipe>> GetRecipesByUserIdAsync(int userId)
        {
            return await _dbContext.RECIPE
                .Where(r => r.UserId == userId)
                .ToListAsync();
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

    }
}
