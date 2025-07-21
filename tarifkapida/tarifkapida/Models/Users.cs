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
        
        // Navigation properties
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
}
