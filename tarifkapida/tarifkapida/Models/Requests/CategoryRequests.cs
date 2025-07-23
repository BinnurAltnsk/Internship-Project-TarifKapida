using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models.Requests
{
    public class CategoryRequests
    {
        [Required]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string CategoryName { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? CategoryDescription { get; set; }
        [Required]
        public int ParentCategoryId { get; set; } // Assuming this is a foreign key to another category
        
    }
}
