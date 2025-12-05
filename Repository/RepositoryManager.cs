using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<SocialAccountRepository> _socialAccountRepository;
        private readonly Lazy<VisitorRepository> _visitorRepository;
        private readonly Lazy<PlaceImageRepository> _placeImageRepository;
        private readonly Lazy<PlaceRepository> _placeRepository;
        private readonly Lazy<CategoryRepository> _categoryRepository;
        private readonly Lazy<ReviewRepository> _reviewRepository;
        private readonly Lazy<PlaceScheduleRepository> _placeScheduleRepository;
        private readonly Lazy<FavouritePlaceRepository> _favouritePlaceRepository;
        private readonly RepositoryContext _context;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _socialAccountRepository = new Lazy<SocialAccountRepository>(() => new SocialAccountRepository(context));
            _visitorRepository = new Lazy<VisitorRepository>(() => new VisitorRepository(context));
            _placeImageRepository = new Lazy<PlaceImageRepository>(() => new PlaceImageRepository(context));
            _placeRepository = new Lazy<PlaceRepository>(() => new PlaceRepository(context));
            _categoryRepository = new Lazy<CategoryRepository>(() => new CategoryRepository(context));
            _reviewRepository = new Lazy<ReviewRepository>(() => new ReviewRepository(context));
            _placeScheduleRepository = new Lazy<PlaceScheduleRepository>(() => new PlaceScheduleRepository(context));
            _favouritePlaceRepository = new Lazy<FavouritePlaceRepository>(() => new FavouritePlaceRepository(context));
        }

        public ISocialAccountRepository SocialAccount => _socialAccountRepository.Value;

        public IVisitorRepository Visitor => _visitorRepository.Value;

        public IPlaceImageRepository PlaceImage => _placeImageRepository.Value;

        public IPlaceRepository Place => _placeRepository.Value;

        public ICategoryRepository Category => _categoryRepository.Value;

        public IReviewRepository Review => _reviewRepository.Value;
        public IPlaceScheduleRepository PlaceSchedule => _placeScheduleRepository.Value;

        public IFavouritePlaceRepository favouritePlace => _favouritePlaceRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        // The implementation of the transaction logic
        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; // Re-throw to let the Controller handle the 500 error
            }
        }
    }
}
