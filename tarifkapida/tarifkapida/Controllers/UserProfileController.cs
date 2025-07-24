using Microsoft.AspNetCore.Mvc;
using tarifkapida.Interfaces;
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

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile(int userId)
        {
            var profile = await userProfileService.GetUserProfileAsync(userId);
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }

        [HttpPost]
        public async Task<ActionResult<UserProfileDto>> CreateOrUpdateUserProfile([FromBody] UserProfileRequest request)
        {
            if (request == null)
                return BadRequest();
            var result = await userProfileService.CreateOrUpdateUserProfileAsync(request);
            return Ok(result);
        }
    }
}
