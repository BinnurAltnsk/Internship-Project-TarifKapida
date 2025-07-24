using tarifkapida.Data;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace tarifkapida.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly AppDbContext _dbContext;

        public UserProfileService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            var profile = await _dbContext.USERPROFILE.FirstOrDefaultAsync(up => up.UserId == userId);
            if (profile == null) return null;
            return new UserProfileDto
            {
                UserProfileId = profile.UserProfileId,
                UserId = profile.UserId,
                ProfileImageUrl = profile.ProfileImageUrl,
                Bio = profile.Bio,
                Instagram = profile.Instagram,
                Facebook = profile.Facebook,
                Twitter = profile.Twitter
            };
        }

        public async Task<UserProfileDto> CreateOrUpdateUserProfileAsync(UserProfileRequest request)
        {
            var profile = await _dbContext.USERPROFILE.FirstOrDefaultAsync(up => up.UserId == request.UserId);
            if (profile == null)
            {
                profile = new UserProfile
                {
                    UserId = request.UserId,
                    ProfileImageUrl = request.ProfileImageUrl,
                    Bio = request.Bio,
                    Instagram = request.Instagram,
                    Facebook = request.Facebook,
                    Twitter = request.Twitter
                };
                _dbContext.USERPROFILE.Add(profile);
            }
            else
            {
                profile.ProfileImageUrl = request.ProfileImageUrl;
                profile.Bio = request.Bio;
                profile.Instagram = request.Instagram;
                profile.Facebook = request.Facebook;
                profile.Twitter = request.Twitter;
                _dbContext.USERPROFILE.Update(profile);
            }
            await _dbContext.SaveChangesAsync();
            return new UserProfileDto
            {
                UserProfileId = profile.UserProfileId,
                UserId = profile.UserId,
                ProfileImageUrl = profile.ProfileImageUrl,
                Bio = profile.Bio,
                Instagram = profile.Instagram,
                Facebook = profile.Facebook,
                Twitter = profile.Twitter
            };
        }
    }
}
