using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3laFein.Reprsentaion.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ReviewController(IServiceManager serviceManager) => _serviceManager = serviceManager;
        [HttpGet("{reviewId:Guid}", Name = "GetReview")]
        public async Task<IActionResult> GetReview([FromRoute] Guid reviewId)
        {
            var review = await _serviceManager.ReviewService.GetReviewAsync(reviewId, false); 
            return Ok(review);
        }

        [HttpGet("visitor/{visitorId:Guid}")]
        public async Task<IActionResult> GetVisitorReviews([FromRoute] Guid visitorId)
        {
            var reviews = await _serviceManager.ReviewService.GetVisitorReviews(visitorId, false);
            return Ok(reviews);
        }

        [HttpGet("place/{placeId:guid}")]
        public async Task<IActionResult> GetPlaceReviews([FromRoute] Guid placeId)
        {
            var reviews = await _serviceManager.ReviewService.GetPlaceReviews(placeId, false);
            return Ok(reviews);
        }

        [HttpPost("place/{placeId:guid}")]
        public async Task<IActionResult> CreateReview([FromRoute] Guid placeId,
            [FromBody] ReviewForCreationDto reviewForCreationDto)
        {
            var visitorIdClaim = User.FindFirst("VisitorId");

            if (visitorIdClaim == null || !Guid.TryParse(visitorIdClaim.Value, out Guid visitorId))
            {
                return Unauthorized("Visitor ID not found or invalid.");
            }


            var review = await _serviceManager.ReviewService.CreateReviewAsync(placeId, reviewForCreationDto, visitorId);
            return CreatedAtRoute("GetReview", new { reviewId = review.ReviewId }, review);
        }

        [HttpPut("{reviewId:guid}")]
        public async Task<IActionResult> UpdateReview([FromRoute] Guid reviewId,
            [FromBody] ReviewForUpdateDto reviewForUpdateDto)
        {
            var visitorIdClaim = User.FindFirst("VisitorId");

            if (visitorIdClaim == null || !Guid.TryParse(visitorIdClaim.Value, out Guid visitorId))
            {
                return Unauthorized("Visitor ID not found or invalid.");
            }

            await _serviceManager.ReviewService.UpdateReviewAsync(reviewId, reviewForUpdateDto, visitorId, trackChanges: true);

            return NoContent();
        }

        [HttpDelete("{reviewId:guid}")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            bool isAdmin = User.IsInRole("Admin");

            var visitorIdClaim = User.FindFirst("VisitorId");
            Guid visitorId = Guid.Empty;

            if (visitorIdClaim != null)
            {
                Guid.TryParse(visitorIdClaim.Value, out visitorId);
            }

            await _serviceManager.ReviewService.DeleteReviewAsync(reviewId, visitorId, isAdmin, trackChanges: false);

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.TryAdd("Allow", "GET, POST, PUT, DELETE, OPTIONS");
            return Ok();
        }
    }
}
