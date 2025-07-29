using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models.Requests
{
    public class SocialAccountRequest
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Provider { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string ProviderUserId { get; set; } = string.Empty;
        [StringLength(200)]
        public string? AccessToken { get; set; }
        [StringLength(200)]
        public string? RefreshToken { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string AccountId { get; internal set; }
    }
}