using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models.Requests
{
    public class UserProfileRequests
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }= string.Empty;
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; } 

    }

    public class UserProfileRequest
    {
        public int UserId { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
    }

    public class UserProfileDto
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
    }
}
