using Microsoft.AspNetCore.Mvc;
using tarifkapida.Interfaces;
using tarifkapida.Models.DTOs;
using tarifkapida.Models.Requests;

namespace tarifkapida.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        readonly IUserProfileService userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        [HttpGet("GetUserProfile/{userId}")]
        public async Task<ActionResult<UserProfileDto?>> GetUserProfile(int userId)
        {
            var profile = await userProfileService.GetUserProfileAsync(userId);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }
        [HttpPost("CreateUserProfile")]
        public async Task<ActionResult<UserProfileDto>> CreateUserProfile([FromBody] UserProfileRequest request)
        {
            if (request == null || request.UserId <= 0)
            {
                return BadRequest("Invalid user profile data.");
            }
            var createdProfile = await userProfileService.CreateOrUpdateUserProfileAsync(request);
            return CreatedAtAction(nameof(GetUserProfile), new { userId = createdProfile.UserId }, createdProfile);
        }
        [HttpPut("UpdateUserProfile")]
        public async Task<ActionResult<UserProfileDto>> UpdateUserProfile([FromBody] UserProfileRequest request)
        {
            if (request == null || request.UserId <= 0)
            {
                return BadRequest("Invalid user profile data.");
            }
            var updatedProfile = await userProfileService.CreateOrUpdateUserProfileAsync(request);
            return Ok(updatedProfile);
        }
        [HttpPost("UploadProfilePhoto")]
        public async Task<IActionResult> UploadProfilePhoto([FromForm] FormFile file, [FromForm] int userId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            var profile = await userProfileService.GetUserProfileAsync(userId);
            if (profile == null)
            {
                return NotFound("User profile not found.");
            }
            var photoUrl = await SaveFileAsync(file);
            await userProfileService.UpdateProfilePhotoAsync(userId, photoUrl);
            return Ok(new { ProfileImageUrl = photoUrl });
        }
        [HttpGet("GetProfilePhoto/{userId}")]
        public async Task<IActionResult> GetProfilePhoto(int userId)
        {
            var profile = await userProfileService.GetUserProfileAsync(userId);
            if (profile == null || string.IsNullOrEmpty(profile.ProfileImageUrl))
            {
                return NotFound("Profile photo not found.");
            }
            var photoStream = await GetFileStreamAsync(profile.ProfileImageUrl);
            if (photoStream == null)
            {
                return NotFound("Photo file not found.");
            }
            return File(photoStream, "image/jpeg");
        }
        [HttpDelete("DeleteProfilePhoto/{userId}")]
        public async Task<IActionResult> DeleteProfilePhoto(int userId)
        {
            var profile = await userProfileService.GetUserProfileAsync(userId);
            if (profile == null || string.IsNullOrEmpty(profile.ProfileImageUrl))
            {
                return NotFound("Profile photo not found.");
            }
            await DeleteFileAsync(profile.ProfileImageUrl);
            await userProfileService.DeleteProfilePhotoAsync(userId);
            return NoContent();
        }
        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "ProfilePhoto");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/images/ProfilePhoto/{uniqueFileName}";
        }
        private async Task<Stream?> GetFileStreamAsync(string filePath)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
                return null;

            return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        }

        private async Task DeleteFileAsync(string filePath)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
