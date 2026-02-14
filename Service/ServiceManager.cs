using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<AuthenticationService> _authenticationService;
        private readonly Lazy<VisitorService> _visitorService;
        private readonly Lazy<SocialAccountService> _socialAccountService;
        private readonly Lazy<ImageService> _imageService;
        private readonly Lazy<PlaceImageService> _placeImageService;
        private readonly Lazy<CategoryService> _categoryService;
        private readonly Lazy<PlaceService> _placeService;
        private readonly Lazy<ReviewService> _reviewService;
        private readonly Lazy<TokenService> _tokenService;
        private readonly Lazy<PlaceScheduleService> _placeScheduleService;
        private readonly Lazy<FavouritePlaceService> _favouritePlaceService;

        public ServiceManager(
                IConfiguration config,
                UserManager<User> userManager,
                IOptions<AppSettings> appSettings,
                IRepositoryManager repositoryManager,
                IMapper mapper,
                IWebHostEnvironment webHost,
                IHttpClientFactory clientFactory)
        {
            _visitorService = new Lazy<VisitorService>(() => new VisitorService(repositoryManager, mapper, userManager, webHost));
            _socialAccountService = new Lazy<SocialAccountService>(() => new SocialAccountService(repositoryManager, mapper));
            _imageService = new Lazy<ImageService>(() => new ImageService(webHost));
            _placeImageService = new Lazy<PlaceImageService>(() => new PlaceImageService(repositoryManager, mapper));
            _categoryService = new Lazy<CategoryService>(() => new CategoryService(repositoryManager, mapper));
            _placeService = new Lazy<PlaceService>(() => new PlaceService(repositoryManager, mapper, clientFactory));
            _reviewService = new Lazy<ReviewService>(() => new ReviewService(repositoryManager, mapper, userManager));
            _tokenService = new Lazy<TokenService>(() => new TokenService(appSettings, userManager));
            _authenticationService = new Lazy<AuthenticationService>(() => new AuthenticationService(userManager, _tokenService.Value, repositoryManager, config));
            _placeScheduleService = new Lazy<PlaceScheduleService>(() => new PlaceScheduleService(repositoryManager, mapper));
            _favouritePlaceService = new Lazy<FavouritePlaceService>(() => new FavouritePlaceService(repositoryManager, mapper));
        }

        public IVisitorService VisitorService => _visitorService.Value;

        public ISocialAccountService SocialAccountService => _socialAccountService.Value;

        public IImageService ImageService => _imageService.Value;

        public IPlaceImageService PlaceImageService => _placeImageService.Value;

        public ICategoryService CategoryService => _categoryService.Value;

        public IPlaceService PlaceService => _placeService.Value;

        public IReviewService ReviewService => _reviewService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IPlaceScheduleService PlaceScheduleService => _placeScheduleService.Value;

        public IFavouritePlaceService FavoritePlaceService => _favouritePlaceService.Value;
    }
}
