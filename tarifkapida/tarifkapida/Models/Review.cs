using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public Recipe Recipe { get; set; } // Navigation property for related Recipe
        public int RecipeId { get; set; } // Foreign key to Recipe table
        public int UserId { get; set; } // Foreign key to Users table
        public string ReviewText { get; set; }
        public int Rating { get; set; } // Rating out of 5
        public DateTime ReviewCreatedAt { get; set; } = DateTime.Now;
        public DateTime ReviewUpdatedAt { get; set; } = DateTime.Now;
    }
}
