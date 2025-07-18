using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }

        public List<Recipe>? Recipe { get; set; } // Navigation property for related Recipe
        public int? RecipeId { get; set; } // Foreign key to Recipe table

    }
}
