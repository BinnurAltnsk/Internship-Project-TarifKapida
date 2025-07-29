using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Services
{
    internal class SocialAccount
    {
        public string Provider { get; set; }
        public object AccountId { get; set; }
        public int UserId { get; set; }
    }
}