using Microsoft.EntityFrameworkCore;
using tarifkapida.Data;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.DTOs;
using tarifkapida.Models.Requests;

namespace tarifkapida.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly AppDbContext _dbContext;

        public UserProfileService(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            var profile = await _dbContext.USERPROFILE
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                return null;

            return MapToDto(profile);
        }

        public async Task<UserProfileDto> CreateOrUpdateUserProfileAsync(UserProfileRequest request)
        {
            var existingProfile = await _dbContext.USERPROFILE
                .FirstOrDefaultAsync(p => p.UserId == request.UserId);

            if (existingProfile == null)
            {
                // Yeni profil oluştur
                var newProfile = new UserProfile
                {
                    UserId = request.UserId,
                    Username = request.Username,
                    Email = request.Email,
                    Bio = request.Bio,
                    Location = request.Location,
                    Website = request.Website,
                    PhoneNumber = request.PhoneNumber,
                    DateOfBirth = request.DateOfBirth,
                    ProfileImageUrl = request.ProfileImageUrl,
                    CreatedAt = DateTime.UtcNow
                };

                _dbContext.USERPROFILE.Add(newProfile);
                await _dbContext.SaveChangesAsync();

                return MapToDto(newProfile);
            }
            else
            {
                // Mevcut profili güncelle
                existingProfile.Username = request.Username ?? existingProfile.Username;
                existingProfile.Email = request.Email ?? existingProfile.Email;
                existingProfile.Bio = request.Bio ?? existingProfile.Bio;
                existingProfile.Location = request.Location ?? existingProfile.Location;
                existingProfile.Website = request.Website ?? existingProfile.Website;
                existingProfile.PhoneNumber = request.PhoneNumber ?? existingProfile.PhoneNumber;
                existingProfile.DateOfBirth = request.DateOfBirth ?? existingProfile.DateOfBirth;
                existingProfile.ProfileImageUrl = request.ProfileImageUrl ?? existingProfile.ProfileImageUrl;
                existingProfile.UpdatedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                return MapToDto(existingProfile);
            }
        }

        public async Task<UserProfileDto> UpdateProfilePhotoAsync(int userId, string imageUrl)
        {
            var profile = await _dbContext.USERPROFILE
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                throw new InvalidOperationException("User profile not found");

            profile.ProfileImageUrl = imageUrl;
            profile.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return MapToDto(profile);
        }

        public async Task<UserProfileDto> DeleteProfilePhotoAsync(int userId)
        {
            var profile = await _dbContext.USERPROFILE
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                throw new InvalidOperationException("User profile not found");

            profile.ProfileImageUrl = null;
            profile.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return MapToDto(profile);
        }

        public async Task<bool> ProfileExistsAsync(int userId)
        {
            return await _dbContext.USERPROFILE
                .AnyAsync(p => p.UserId == userId);
        }

        private static UserProfileDto MapToDto(UserProfile profile)
        {
            return new UserProfileDto
            {
                UserId = profile.UserId,
                Username = profile.Username,
                Email = profile.Email,
                Bio = profile.Bio,
                Location = profile.Location,
                Website = profile.Website,
                PhoneNumber = profile.PhoneNumber,
                DateOfBirth = profile.DateOfBirth,
                ProfileImageUrl = profile.ProfileImageUrl,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt
            };
        }
    }
}