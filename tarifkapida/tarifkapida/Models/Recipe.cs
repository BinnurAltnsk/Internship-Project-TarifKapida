namespace tarifkapida.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeIngredients { get; set; }
        public string RecipeInstructions { get; set; }
        public string RecipeImageUrl { get; set; }
        public int? UserId { get; set; } // Foreign key to Users table
        public DateTime? RecipeCreatedAt { get; set; } = DateTime.Now;
        public DateTime? RecipeUpdatedAt { get; set; } = DateTime.Now;
        public List<Review>? Reviews { get; set; }
    }
}
