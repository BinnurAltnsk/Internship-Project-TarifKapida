namespace tarifkapida.Models.DTO
{
    public class FavoriteDto
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string RecipeName { get; set; } = string.Empty;
        public string? RecipeImageUrl { get; set; }
        public DateTime FavoritedAt { get; set; }
    }
}