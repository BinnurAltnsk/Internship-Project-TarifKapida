// Models/Requests/UserProfileRequest.cs

using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models.Requests
{
    public class UserProfileRequest
    {
        [Required]
        public int UserId { get; set; }

        [StringLength(50)]
        public string? Username { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(500)]
        public string? Bio { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        [StringLength(200)]
        public string? Website { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? ProfileImageBase64 { get; set; }
    }
}
