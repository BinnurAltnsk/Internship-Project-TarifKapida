using tarifkapida.Data;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace tarifkapida.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _dbContext;

        public ReviewService(AppDbContext context)
        {
            _dbContext = context;
        }
        public List<Review> GetReviews()
        {
            return _dbContext.REVIEW
        .Include(r => r.User)
        .Include(r => r.Recipe)
        .ToList();
        }
        public List<Review> GetReviewsByRecipeId(int recipeId)
        {
            return _dbContext.REVIEW.Where(r => r.RecipeId == recipeId).ToList();
        }
        public List<Review> GetReviewsByUserId(int userId)
        {
            return _dbContext.REVIEW.Where(r => r.UserId == userId).ToList();
        }
        public async Task<Review?> GetReviewByIdAsync(int reviewId)
        {
            return await _dbContext.REVIEW.FindAsync(reviewId);
        }
        public async Task<Review> CreateReviewAsync(Review review)
        {
            _dbContext.REVIEW.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
        public async Task<Review> UpdateReviewAsync(Review review)
        {
            _dbContext.REVIEW.Update(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await _dbContext.REVIEW.FindAsync(reviewId);
            if (review == null)
                return false;
            _dbContext.REVIEW.Remove(review);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public List<ReviewDto> GetReviewDtos()
        {
            return _dbContext.REVIEW
                .Include(r => r.User)
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    ReviewText = r.ReviewText,
                    Rating = r.Rating,
                    UserId = r.UserId,
                    Username = r.User.Username,
                    ReviewCreatedAt = r.ReviewCreatedAt,
                    ReviewUpdatedAt = r.ReviewUpdatedAt
                }).ToList();
        }
    }
}
