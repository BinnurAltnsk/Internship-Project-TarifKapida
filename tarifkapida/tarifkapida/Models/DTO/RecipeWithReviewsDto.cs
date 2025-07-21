namespace tarifkapida.Models.DTO
{
    public class RecipeWithReviewsDto
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeIngredients { get; set; }
        public string RecipeInstructions { get; set; }
        public string RecipeImageUrl { get; set; }
        public DateTime? RecipeCreatedAt { get; set; }
        public DateTime? RecipeUpdatedAt { get; set; }

        public List<ReviewDto> Reviews { get; set; } = new();
    }
}
