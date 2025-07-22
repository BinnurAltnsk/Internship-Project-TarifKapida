using tarifkapida.Models;
using tarifkapida.Models.Requests;

namespace tarifkapida.Interfaces
{
    public interface IUserService
    {        
        Task<Users?> GetUserByIdAsync(int userId);
        Task<Users> CreateUserAsync(Users user);
        Task<Users> UpdateUserAsync(Users user);
        Task<bool> DeleteUserAsync(int userId);        
        Task<Users?> LoginAsync(LoginRequest loginRequest);
        Task<Users?> GetUserByUsernameAsync(string username);
        Task<Users> RegisterAsync(RegisterRequest request);
        List<Users> GetUsers();
    }
}
