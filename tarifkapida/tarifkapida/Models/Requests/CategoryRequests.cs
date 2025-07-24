using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models.Requests
{
    public class CategoryRequests
    {
        [Required]
        public string CategoryName { get; set; }
        public int RecipeId { get; set; }
        public CategoryRequests(string categoryName, int recipeId)
        {
            CategoryName = categoryName;
            RecipeId = recipeId;
        }
        public string? CategoryDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
