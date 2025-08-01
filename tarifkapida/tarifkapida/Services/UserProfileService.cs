using Microsoft.EntityFrameworkCore;
using tarifkapida.Data;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.DTO;
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

        public async Task<UserProfileDto> UpdateUserProfileAsync(UserProfileRequest request)
        {
            var profile = await _dbContext.USERPROFILE
                .FirstOrDefaultAsync(p => p.UserId == request.UserId);

            if (profile == null)
                throw new InvalidOperationException("User profile not found");

            profile.Username = request.Username;
            profile.Email = request.Email;
            profile.Bio = request.Bio;
            profile.Location = request.Location;
            profile.Website = request.Website;
            profile.PhoneNumber = request.PhoneNumber;
            profile.DateOfBirth = request.DateOfBirth;
            profile.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return MapToDto(profile);
        }

        public async Task<UserProfileDto> CreateUserProfileAsync(UserProfileRequest request)
        {
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
                CreatedAt = DateTime.UtcNow
            };
            _dbContext.USERPROFILE.Add(newProfile);
            await _dbContext.SaveChangesAsync();
            return MapToDto(newProfile);
        }

        public async Task<bool> ProfileExistsAsync(int userId)
        {
            return await _dbContext.USERPROFILE.AnyAsync(p => p.UserId == userId);
        }

        public async Task<UserProfileDto> UploadUserProfilePhotoBase64Async(UserProfileRequest request)
        {
            var profile = await _dbContext.USERPROFILE.FirstOrDefaultAsync(x => x.UserId == request.UserId);
            if (profile == null)
                throw new Exception("User profile not found");

            if (!string.IsNullOrEmpty(request.ProfileImageBase64) && request.ProfileImageBase64.StartsWith("data:image"))
            {
                string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string imagesFolder = Path.Combine(wwwRootPath, "images", "ProfilePhoto");

                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                string fileName = $"{request.UserId}_{Guid.NewGuid()}.jpg";
                string filePath = Path.Combine(imagesFolder, fileName);

                var base64Data = request.ProfileImageBase64.Substring(request.ProfileImageBase64.IndexOf(",") + 1);
                byte[] imageBytes = Convert.FromBase64String(base64Data);
                await File.WriteAllBytesAsync(filePath, imageBytes);

                profile.ProfileImageBase64 = $"/images/ProfilePhoto/{fileName}";
                profile.UpdatedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }

            return MapToDto(profile);
        }

        public async Task<string> GetUserProfilePhotoAsync(int userId)
        {
            var profile = await _dbContext.USERPROFILE
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
                throw new InvalidOperationException("User profile not found");

            return profile.ProfileImageBase64 ?? string.Empty;
        }


        public async Task<bool> DeleteUserProfilePhotoAsync(int userId)
        {
            var profile = await _dbContext.USERPROFILE
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null || string.IsNullOrEmpty(profile.ProfileImageBase64))
                throw new InvalidOperationException("User profile or photo not found");

            // Dosyayı sistemden sil
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profile.ProfileImageBase64.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            // Veritabanındaki referansı kaldır
            profile.ProfileImageBase64 = null;
            profile.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File cannot be null or empty");

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var uploadsFolder = Path.Combine("wwwroot", "images", "ProfilePhoto");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"/images/ProfilePhoto/{fileName}";
        }

        public async Task<UserProfileDto> UpdateNotificationSettingsAsync(int userId, NotificationSettingsRequest request)
        {
            var profile = await _dbContext.USERPROFILE.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                throw new InvalidOperationException("User profile not found");

            profile.NotificationSettings = (NotificationSettings?)request.NotificationSettings;
            profile.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return MapToDto(profile);
        }

        public async Task<NotificationSettingsDto> GetNotificationSettingsAsync(int userId)
        {
            var profile = await _dbContext.USERPROFILE.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                throw new InvalidOperationException("User profile not found");

            return new NotificationSettingsDto
            {
                UserId = profile.UserId,
                NotificationSettings = profile.NotificationSettings
            };
        }

        public async Task<UserProfileDto> LinkSocialAccountAsync(int userId, string provider, SocialAccountRequest request)
        {
            var profile = await _dbContext.USERPROFILE
                .Include(p => p.LinkedSocialAccounts)
                .FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                throw new InvalidOperationException("User profile not found");

            var linkedSocialAccounts = profile.LinkedSocialAccounts as ICollection<LinkedSocialAccount>;
            if (linkedSocialAccounts == null)
                throw new InvalidOperationException("Linked social accounts collection is not initialized");

            if (!linkedSocialAccounts.Any(a => a.Provider == request.Provider && a.AccountId == request.AccountId))
            {
                var socialAccount = new LinkedSocialAccount
                {
                    Provider = request.Provider,
                    AccountId = request.AccountId
                };
                linkedSocialAccounts.Add(socialAccount);
                profile.UpdatedAt = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
                return MapToDto(profile);
            }

            throw new InvalidOperationException("Social account already linked");
        }

        public async Task<UserProfileDto> UnlinkSocialAccountAsync(int userId, string provider)
        {
            var profile = await _dbContext.USERPROFILE
                .Include(p => p.LinkedSocialAccounts)
                .FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                throw new InvalidOperationException("User profile not found");

            var linkedSocialAccounts = profile.LinkedSocialAccounts as ICollection<LinkedSocialAccount>;
            if (linkedSocialAccounts == null)
                throw new InvalidOperationException("Linked social accounts collection is not initialized");

            var account = linkedSocialAccounts.FirstOrDefault(a => a.Provider == provider);
            if (account == null)
                throw new InvalidOperationException("Social account not linked");

            linkedSocialAccounts.Remove(account);
            profile.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return MapToDto(profile);
        }

        public async Task<List<SocialAccountDto>> GetLinkedSocialAccountsAsync(int userId)
        {
            var profile = await _dbContext.USERPROFILE
                .Include(p => p.LinkedSocialAccounts)
                .FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                throw new InvalidOperationException("User profile not found");

            var linkedSocialAccounts = profile.LinkedSocialAccounts as ICollection<LinkedSocialAccount>;
            if (linkedSocialAccounts == null)
                throw new InvalidOperationException("Linked social accounts collection is not initialized");

            return linkedSocialAccounts.Select(a => new SocialAccountDto
            {
                Provider = a.Provider,
                AccountId = a.AccountId
            }).ToList();
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
                ProfileImageBase64 = profile.ProfileImageBase64,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt
            };
        }
    }
}