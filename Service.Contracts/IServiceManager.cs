namespace Service.Contracts
{
    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }
        IVisitorService VisitorService { get; }
        ISocialAccountService SocialAccountService { get; }
        IImageService ImageService { get; }
        IPlaceImageService PlaceImageService { get; }
        IPlaceService PlaceService { get; }
        ICategoryService CategoryService { get; }
        IReviewService ReviewService { get; }
        IPlaceScheduleService PlaceScheduleService { get; }
        IFavouritePlaceService FavoritePlaceService { get; }
    }
}
