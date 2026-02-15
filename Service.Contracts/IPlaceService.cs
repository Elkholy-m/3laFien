using Entities.Models;
using Shared.DTO;

namespace Service.Contracts
{
    public interface IPlaceService
    {
        Task<IEnumerable<Place>> GetAllPlacesToRebuildIndex();
        Task<(IEnumerable<PlaceDto> placeDtos, MetaData metaData)> 
            GetPlacesAsync(PlaceQueryString queryString,
                    bool trackChanges, ISearchService searchService);

        Task<PlaceDto> GetPlaceAsync(Guid placeId, bool trackChanges);
        Task<PlaceDto> CreatePlaceAsync(PlaceForCreationDto placeForCreationDto , ISearchService searchService);
        Task<PlaceDto> UpdatePlaceAsync(Guid placeId, PlaceForUpdateDto placeForUpdateDto,
                bool trackChanges, ISearchService searchService);
        Task DeletePlaceAsync(Guid placeId, bool trackChanges, ISearchService searchService);
        Task<IEnumerable<PlaceDto>> GetNearestPlaces(double userLon, double userLat, bool trackChanges);
    }
}
