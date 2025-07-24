using Microsoft.AspNetCore.Mvc;
using tarifkapida.Interfaces;
using tarifkapida.Models;

namespace tarifkapida.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            this.favoriteService = favoriteService;
        }

        [HttpPost("AddToFavorites")]
        public async Task<IActionResult> AddToFavorites([FromQuery] int userId, [FromQuery] int recipeId)
        {
            if (userId <= 0 || recipeId <= 0)
            {
                return BadRequest("Geçersiz kullanıcı ID veya tarif ID");
            }

            var result = await favoriteService.AddToFavoritesAsync(userId, recipeId);
            
            if (result)
            {
                return Ok(new { message = "Tarif favorilere eklendi" });
            }
            else
            {
                return BadRequest("Tarif favorilere eklenemedi. Tarif zaten favorilerde olabilir.");
            }
        }

        [HttpPost("RemoveFromFavorites")]
        public async Task<IActionResult> RemoveFromFavorites([FromQuery] int userId, [FromQuery] int recipeId)
        {
            if (userId <= 0 || recipeId <= 0)
            {
                return BadRequest("Geçersiz kullanıcı ID veya tarif ID");
            }

            var result = await favoriteService.RemoveFromFavoritesAsync(userId, recipeId);
            
            if (result)
            {
                return Ok(new { message = "Tarif favorilerden çıkarıldı" });
            }
            else
            {
                return BadRequest("Tarif favorilerden çıkarılamadı.");
            }
        }

        [HttpGet("GetUserFavorites/{userId}")]
        public async Task<ActionResult<List<Recipe>>> GetUserFavorites(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Geçersiz kullanıcı ID");
            }

            var favorites = await favoriteService.GetUserFavoritesAsync(userId);
            return Ok(favorites);
        }

        [HttpGet("IsFavorite")]
        public async Task<IActionResult> IsFavorite([FromQuery] int userId, [FromQuery] int recipeId)
        {
            if (userId <= 0 || recipeId <= 0)
            {
                return BadRequest("Geçersiz kullanıcı ID veya tarif ID");
            }

            var isFavorite = await favoriteService.IsFavoriteAsync(userId, recipeId);
            return Ok(new { isFavorite = isFavorite });
        }

        [HttpPost("ToggleFavorite")]
        public async Task<IActionResult> ToggleFavorite([FromQuery] int userId, [FromQuery] int recipeId)
        {
            if (userId <= 0 || recipeId <= 0)
            {
                return BadRequest("Geçersiz kullanıcı ID veya tarif ID");
            }

            var isFavorite = await favoriteService.IsFavoriteAsync(userId, recipeId);
            
            if (isFavorite)
            {
                var removeResult = await favoriteService.RemoveFromFavoritesAsync(userId, recipeId);
                if (removeResult)
                {
                    return Ok(new { message = "Tarif favorilerden çıkarıldı", isFavorite = false });
                }
            }
            else
            {
                var addResult = await favoriteService.AddToFavoritesAsync(userId, recipeId);
                if (addResult)
                {
                    return Ok(new { message = "Tarif favorilere eklendi", isFavorite = true });
                }
            }

            return BadRequest("İşlem başarısız");
        }

        [HttpGet("SearchUserFavorites")]
        public async Task<ActionResult<List<Recipe>>> SearchUserFavorites([FromQuery] int userId, [FromQuery] string searchTerm)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Geçersiz kullanıcı ID veya arama terimi");
            }

            var filteredFavorites = await favoriteService.SearchUserFavoritesAsync(userId, searchTerm);
            return Ok(filteredFavorites);
        }
    }
}