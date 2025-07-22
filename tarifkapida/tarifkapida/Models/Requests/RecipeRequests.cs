using System.ComponentModel.DataAnnotations;


namespace tarifkapida.Models.Requests
{
    public class RecipeDto
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeIngredients { get; set; }
        public string RecipeInstructions { get; set; }
        public string RecipeImageUrl { get; set; }
        public int? UserId { get; set; }
        public DateTime? RecipeCreatedAt { get; set; }
        public DateTime? RecipeUpdatedAt { get; set; }
        public List<ReviewDto>? Reviews { get; set; }
    }

    public class RecipeWithReviewsDto
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeIngredients { get; set; }
        public string RecipeInstructions { get; set; }
        public string RecipeImageUrl { get; set; }
        public int? UserId { get; set; }
        public DateTime? RecipeCreatedAt { get; set; }
        public DateTime? RecipeUpdatedAt { get; set; }
        public List<ReviewDto>? Reviews { get; set; }
        public int ReviewCount { get; set; }
        public double AverageRating { get; set; }
    }

    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewCreatedAt { get; set; }
        public DateTime ReviewUpdatedAt { get; set; }
        public string? Username { get; set; }
    }
}
