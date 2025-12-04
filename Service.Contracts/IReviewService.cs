using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetReviewsAsync(bool trackChanges);
        Task<ReviewDto> GetReviewAsync(Guid reviewId, bool trackChanges);
        Task<IEnumerable<ReviewDto>> GetPlaceReviews(Guid placeId, bool trackChanges);
        Task<IEnumerable<ReviewDto>> GetVisitorReviews(Guid visitorId, bool trackChanges);
        Task<ReviewDto> CreateReviewAsync(Guid placeId, ReviewForCreationDto reviewForCreationDto, Guid vistiorId);
        // Update: Only VisitorId needed (Admin shouldn't update)
        Task UpdateReviewAsync(Guid reviewId, ReviewForUpdateDto dto, Guid requesterVisitorId, bool trackChanges);

        // Delete: Needs VisitorId AND Admin check
        Task DeleteReviewAsync(Guid reviewId, Guid requesterVisitorId, bool isAdmin, bool trackChanges);

    }
}
