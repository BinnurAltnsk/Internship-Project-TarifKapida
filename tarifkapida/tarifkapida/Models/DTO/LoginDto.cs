namespace tarifkapida.Models.DTO
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public LoginDto()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
