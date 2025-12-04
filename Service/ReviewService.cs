using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ReviewService : IReviewService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ReviewService(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ReviewDto> CreateReviewAsync(Guid placeId, ReviewForCreationDto reviewForCreationDto, Guid vistiorId)
        {
            var place = await _repositoryManager.Place.GetPlaceAsync(placeId, false);
            if (place is null)
                throw new PlaceNotFoundException(placeId);
            var reviewEntity = _mapper.Map<Review>(reviewForCreationDto);
            reviewEntity.PlaceId = placeId;
            reviewEntity.VisitorId = vistiorId;
            reviewEntity.CreatedAt = DateTime.UtcNow;
            _repositoryManager.Review.CreateReviewAsync(reviewEntity);
            await _repositoryManager.SaveAsync();
            return _mapper.Map<ReviewDto>(reviewEntity);
        }

        public async Task DeleteReviewAsync(Guid reviewId, Guid requesterVisitorId, bool isAdmin, bool trackChanges)
        {
            var review = await CheckReviewExistance(reviewId, trackChanges);
            // Security Check
            // If NOT Admin AND IDs don't match => Forbidden
            if (!isAdmin && review.VisitorId != requesterVisitorId)
            {
                throw new ForbiddenActionException("You are not allowed to delete this review.");
            }

            _repositoryManager.Review.DeleteReview(review);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<ReviewDto>> GetPlaceReviews(Guid placeId, bool trackChanges)
        {
            var place = await _repositoryManager.Place.GetPlaceAsync(placeId, false);
            if (place is null) throw new PlaceNotFoundException(placeId);

            var reviews = await _repositoryManager.Review.GetReviewsAsyncByPlaceId(placeId, trackChanges);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto> GetReviewAsync(Guid reviewId, bool trackChanges)
        {
            var review = await CheckReviewExistance(reviewId, trackChanges);
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsAsync(bool trackChanges)
        {
            var reviews = await _repositoryManager.Review.GetReviewsAsync(trackChanges);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<IEnumerable<ReviewDto>> GetVisitorReviews(Guid visitorId, bool trackChanges)
        {
            var visitor = _repositoryManager.Visitor.GetVisitorAsync(visitorId, false);
            if (visitor is null) throw new VisitorNotFoundException(visitorId);
            var reviews = await _repositoryManager.Review.GetReviewsAsyncByVisitorId(visitorId, trackChanges);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task UpdateReviewAsync(Guid reviewId, ReviewForUpdateDto reviewForUpdateDto, Guid requesterVisitorId, bool trackChanges)
        {
            var review = await CheckReviewExistance(reviewId, trackChanges);

            // Security Check: Compare Guids
            if (review.VisitorId != requesterVisitorId)
            {
                throw new ForbiddenActionException("You can only edit your own reviews.");
            }
            _mapper.Map(reviewForUpdateDto, review);
            _repositoryManager.Review.UpdateReview(review);
            await _repositoryManager.SaveAsync();
        }
        private async Task<Review> CheckReviewExistance(Guid reviewId, bool trackChanges)
        {
            var review = await _repositoryManager.Review.GetReviewAsync(reviewId, trackChanges);
            if (review is null)
                throw new ReviewNotFoundException(reviewId);
            return review;
        }
    }
}
