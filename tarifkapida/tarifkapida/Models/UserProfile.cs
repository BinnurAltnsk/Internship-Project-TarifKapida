using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tarifkapida.Services;

namespace tarifkapida.Models
{
    [Table("UserProfiles")]
    public class UserProfile
    {
        [Key]
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

        [StringLength(500)]
        public string? ProfileImageBase64 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("UserId")]
        public virtual Users? User { get; set; }

        public virtual NotificationSettings? NotificationSettings { get; set; }

        public virtual LinkedSocialAccount? LinkedSocialAccount { get; set; }

        public ICollection<LinkedSocialAccount> LinkedSocialAccounts { get; internal set; } = new List<LinkedSocialAccount>();
    }
}
