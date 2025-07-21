using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tarifkapida.Models
{
    public class Review
{
        public int ReviewId { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; } // ✅ Bu çok önemli

        public string ReviewText { get; set; }
        public int Rating { get; set; }

        public DateTime ReviewCreatedAt { get; set; }
        public DateTime ReviewUpdatedAt { get; set; }
    }
}
