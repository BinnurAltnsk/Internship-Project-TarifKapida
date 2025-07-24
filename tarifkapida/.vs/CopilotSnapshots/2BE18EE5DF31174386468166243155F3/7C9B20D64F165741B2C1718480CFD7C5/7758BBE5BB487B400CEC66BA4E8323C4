using tarifkapida.Models;
using tarifkapida.Models.Requests;

namespace tarifkapida.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
        Task<UserProfileDto> CreateOrUpdateUserProfileAsync(UserProfileRequest request);
    }
}
