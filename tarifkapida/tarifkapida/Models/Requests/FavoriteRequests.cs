namespace tarifkapida.Models.Requests
{
    public class FavoriteRequests
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public FavoriteRequests(int userId, int recipeId)
        {
            UserId = userId;
            RecipeId = recipeId;
        }
        public FavoriteRequests() { }
        public override string ToString()
        {
            return $"UserId: {UserId}, RecipeId: {RecipeId}";
        }
    }
}
