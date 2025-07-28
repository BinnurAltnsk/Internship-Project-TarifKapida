using tarifkapida.Models;
using tarifkapida.Models.Requests;

namespace tarifkapida.Interfaces
{
    public interface IReviewService
    {
        Task<Review?> GetReviewByIdAsync(int reviewId);
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(int reviewId);
        List<Review> GetReviews();
        List<Review> GetReviewsByRecipeId(int recipeId);
        List<Review> GetReviewsByUserId(int userId);
        Task<object> GetPagedReviewsAsync(int page, int pageSize);
        Task<object> GetPagedReviewsByRecipeAsync(int recipeId, int page, int pageSize);
        List<ReviewDto> GetReviewDtos();
    }
}
