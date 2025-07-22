using tarifkapida.Data;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace tarifkapida.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext context)
        {
            _dbContext = context;
        }

        public List<Users> GetUsers()
        {
            return _dbContext.USER.ToList();
        }        
        public async Task<Users> CreateUserAsync(Users user)
        {
            _dbContext.USER.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<Users> UpdateUserAsync(Users user)
        {
            _dbContext.USER.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _dbContext.USER.FindAsync(userId);
            if (user == null)
                return false;

            _dbContext.USER.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Users?> GetUserByIdAsync(int userId)
        {
            return await _dbContext.USER.FindAsync(userId);
        }
        public async Task<Users?> LoginAsync(LoginRequest loginRequest)
        {
            return await _dbContext.USER.FirstOrDefaultAsync(u => 
                u.Username == loginRequest.Username && 
                u.Password == loginRequest.Password);
        }
        public IActionResult Login(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }
        public async Task<Users?> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.USER.FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<Users> RegisterAsync(RegisterRequest request)
        {
            if(string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentException("Username, Email, and Password are required.");
            }

            if(request.Password != request.ConfirmPassword)
            {
                throw new ArgumentException("Passwords do not match.");
            }

            var user = new Users
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };

            _dbContext.USER.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
