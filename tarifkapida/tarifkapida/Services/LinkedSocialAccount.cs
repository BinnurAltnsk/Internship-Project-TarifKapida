using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Services
{
    public class LinkedSocialAccount
    {
        [Key]
        public int LinkedSocialAccountId { get; set; }
        public string Provider { get; set; }
        public string AccountId { get; set; }
    }
}