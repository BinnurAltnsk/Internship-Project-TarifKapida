using System.ComponentModel.DataAnnotations;


namespace tarifkapida.Models.Requests
{
    public class RecipeRequests
    {
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeIngredients { get; set; }
        public string RecipeInstructions { get; set; }
        public string RecipeImageUrl { get; set; }

    }
}
