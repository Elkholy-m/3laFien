using Contracts;

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
        private readonly Lazy<ExternalCityRepository> _cityRepository;
        private readonly Lazy<ExternalStateRepository> _stateRepository;
        private readonly Lazy<ExternalCountryRepository> _countryRepository;
        private readonly RepositoryContext _context;

        public RepositoryManager(RepositoryContext context, PlaceDbContext placeContext)
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
            _countryRepository = new Lazy<ExternalCountryRepository>(() => new (placeContext));
            _stateRepository = new Lazy<ExternalStateRepository>(() => new (placeContext));
            _cityRepository = new Lazy<ExternalCityRepository>(() => new (placeContext));
        }

        public ISocialAccountRepository SocialAccount => _socialAccountRepository.Value;

        public IVisitorRepository Visitor => _visitorRepository.Value;

        public IPlaceImageRepository PlaceImage => _placeImageRepository.Value;

        public IPlaceRepository Place => _placeRepository.Value;

        public ICategoryRepository Category => _categoryRepository.Value;

        public IReviewRepository Review => _reviewRepository.Value;

        public IPlaceScheduleRepository PlaceSchedule => _placeScheduleRepository.Value;

        public IFavouritePlaceRepository FavouritePlace => _favouritePlaceRepository.Value;

        public IExternalCityRepository CityRepository => _cityRepository.Value;

        public IExternalStateRepository StateRepository => _stateRepository.Value;

        public IExternalCountryRepository CountryRepository => _countryRepository.Value;

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
