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


        // Login method - returns user or null
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

        //public async Task<Users?> GetUserByUsernameAsync(string username)
        //{
        //    return await _dbContext.USER.FirstOrDefaultAsync(u => u.Username == username);
        //}

        //public async Task<Users?> GetUserByEmailAsync(string email)
        //{
        //    return await _dbContext.USER.FirstOrDefaultAsync(u => u.Email == email);
        //}

        //// Authentication & Validation
        //public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        //{
        //    var user = await _dbContext.USER.FirstOrDefaultAsync(u => 
        //        u.Username == username && u.Password == password);
        //    return user != null;
        //}

        //public async Task<bool> IsUsernameExistsAsync(string username)
        //{
        //    return await _dbContext.USER.AnyAsync(u => u.Username == username);
        //}

        //public async Task<bool> IsEmailExistsAsync(string email)
        //{
        //    return await _dbContext.USER.AnyAsync(u => u.Email == email);
        //}

        //// User Management
        //public async Task<List<Users>> GetUsersByRecipeIdAsync(int recipeId)
        //{
        //    return await _dbContext.USER
        //        .Where(u => u.RecipeId == recipeId)
        //        .ToListAsync();
        //}

        //public async Task<bool> ChangePasswordAsync(int userId, string newPassword)
        //{
        //    var user = await _dbContext.USER.FindAsync(userId);
        //    if (user == null)
        //        return false;

        //    user.Password = newPassword;
        //    await _dbContext.SaveChangesAsync();
        //    return true;
        //}
    }
}
