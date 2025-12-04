using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(RepositoryContext context) : base(context)
        {
        }
        public void CreateReviewAsync(Review review) => Create(review);

        public void DeleteReview(Review review) => Delete(review);

        public async Task<IEnumerable<Review>> GetReviewsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<Review?> GetReviewAsync(Guid reviewId, bool trackChanges) =>
            await FindByCondition(r => r.ReviewId == reviewId, trackChanges).SingleOrDefaultAsync();

        public void UpdateReview(Review review) => Update(review);

        public async Task<IEnumerable<Review>> GetReviewsAsyncByPlaceId(Guid placeId, bool trackChanges) => await FindByCondition(r => r.PlaceId == placeId, trackChanges).ToListAsync<Review>();

        public async Task<IEnumerable<Review>> GetReviewsAsyncByVisitorId(Guid visitorId, bool trackChanges) => await FindByCondition(r => r.VisitorId == visitorId, trackChanges).ToListAsync<Review>();
    }
}
