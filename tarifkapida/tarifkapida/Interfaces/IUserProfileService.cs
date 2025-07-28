using tarifkapida.Models.DTOs;
using tarifkapida.Models.Requests;

namespace tarifkapida.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
        Task<UserProfileDto> CreateOrUpdateUserProfileAsync(UserProfileRequest request);
        Task<UserProfileDto> UpdateProfilePhotoAsync(int userId, string imageUrl);
        Task<UserProfileDto> DeleteProfilePhotoAsync(int userId);
        Task<bool> ProfileExistsAsync(int userId);
    }
}