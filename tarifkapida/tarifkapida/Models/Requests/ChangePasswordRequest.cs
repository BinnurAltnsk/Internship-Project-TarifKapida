using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Requests
{
    public class ChangePasswordRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Yeni şifre en az 6 karakter olmalıdır.")]
        public string NewPassword { get; set; }
    }
}
