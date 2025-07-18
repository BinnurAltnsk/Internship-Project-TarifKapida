using System.ComponentModel.DataAnnotations;

namespace tarifkapida.Models.Requests
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}