using tarifkapida.Models.DTO;
using tarifkapida.Models.DTOs;
using tarifkapida.Models.Requests;

namespace tarifkapida.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
        Task<UserProfileDto> UpdateUserProfileAsync(UserProfileRequest request);
        Task<UserProfileDto> CreateUserProfileAsync(UserProfileRequest request);
        Task<bool> ProfileExistsAsync(int userId);
        Task<UserProfileDto> UploadUserProfilePhotoBase64Async(UserProfileRequest request);
        Task<string> GetUserProfilePhotoAsync(int userId);
        Task<bool> DeleteUserProfilePhotoAsync(int userId);
        Task<string> SaveFileAsync(IFormFile file);
        Task<UserProfileDto> UpdateNotificationSettingsAsync(int userId, NotificationSettingsRequest request);
        Task<NotificationSettingsDto> GetNotificationSettingsAsync(int userId);
        Task<UserProfileDto> LinkSocialAccountAsync(int userId, string provider, SocialAccountRequest request);
        Task<UserProfileDto> UnlinkSocialAccountAsync(int userId, string provider);
        Task<List<SocialAccountDto>> GetLinkedSocialAccountsAsync(int userId);
        

    }
}