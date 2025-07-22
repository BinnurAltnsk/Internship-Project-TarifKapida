using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models.Requests
{
    public class ReviewRequests
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }        
        public DateTime ReviewCreatedAt { get; set; } = DateTime.Now;        
        public DateTime ReviewUpdatedAt { get; set; } = DateTime.Now;

    }
}
