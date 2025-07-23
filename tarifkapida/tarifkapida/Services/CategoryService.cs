using tarifkapida.Data;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace tarifkapida.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _dbContext;
        public CategoryService(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _dbContext.CATEGORY
                .Include(c => c.Recipes)
                .ToListAsync();
        }
        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await _dbContext.CATEGORY
                .Include(c => c.Recipes)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _dbContext.CATEGORY.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _dbContext.CATEGORY.FindAsync(category.CategoryId);
            if (existingCategory == null)
            {
                return null;
            }
            existingCategory.CategoryDescription = category.CategoryDescription;
            existingCategory.CategoryDescription = category.CategoryDescription;
            await _dbContext.SaveChangesAsync();
            return existingCategory;
        }
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _dbContext.CATEGORY.FindAsync(categoryId);
            if (category == null)
            {
                return false;
            }
            _dbContext.CATEGORY.Remove(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<Category>> GetCategoriesByParentIdAsync(int parentCategoryId)
        {
            return await _dbContext.CATEGORY
                .Where(c => c.ParentCategoryId == parentCategoryId)
                .ToListAsync();

        }
        public async Task<List<Category>> SearchCategoriesAsync(string searchTerm)
        {
            return await _dbContext.CATEGORY
                .Where(c => c.CategoryName.Contains(searchTerm) || c.CategoryDescription.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
