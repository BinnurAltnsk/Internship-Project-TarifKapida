namespace tarifkapida.Models.DTO
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        // Optional: You can add properties for token or other authentication details if needed
        // public string Token { get; set; }
        // public DateTime TokenExpiration { get; set; }
        
        // Constructor to initialize properties if needed
        public LoginDto()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
