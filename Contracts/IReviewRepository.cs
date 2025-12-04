using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsAsync(bool trackChanges);
        Task<IEnumerable<Review>> GetReviewsAsyncByPlaceId(Guid placeId, bool trackChanges);
        Task<IEnumerable<Review>> GetReviewsAsyncByVisitorId(Guid visitorId, bool trackChanges);
        Task<Review?> GetReviewAsync(Guid reviewId, bool trackChanges);
        void CreateReviewAsync(Review review);
        void UpdateReview(Review review);
        void DeleteReview(Review review);
    }
}
