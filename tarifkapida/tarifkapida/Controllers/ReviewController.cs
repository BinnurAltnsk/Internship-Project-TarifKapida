using Microsoft.AspNetCore.Mvc;
using tarifkapida.Interfaces;
using tarifkapida.Models;
using tarifkapida.Models.Requests;
using tarifkapida.Services;

namespace tarifkapida.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        readonly IReviewService reviewService;
        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet("GetReviews")]
        public List<Review> GetReviews()
        {
            return reviewService.GetReviews();
        }

        [HttpGet("GetReviewById/{reviewId}")]
        public async Task<ActionResult<Review?>> GetReviewById(int reviewId)
        {
            var review = await reviewService.GetReviewByIdAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }
        [HttpPost("CreateReview")]
        public async Task<ActionResult<Review>> CreateReview([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest("Review data is null");
            }
            var createdReview = await reviewService.CreateReviewAsync(review);
            return CreatedAtAction(nameof(GetReviewById), new { reviewId = createdReview.ReviewId }, createdReview);
        }
        [HttpPut("UpdateReview")]
        public async Task<ActionResult<Review>> UpdateReview([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest("Review data is null");
            }
            var updatedReview = await reviewService.UpdateReviewAsync(review);
            return Ok(updatedReview);
        }
        [HttpPost("DeleteReview/{reviewId}")]
        public async Task<IActionResult> DeleteReview([FromQuery] int reviewId)
        {
            var result = await reviewService.DeleteReviewAsync(reviewId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
