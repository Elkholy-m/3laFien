using Entities.Models;
using Shared.DTO;

namespace Service.Contracts
{
    public interface IPlaceService
    {
        Task<(IEnumerable<PlaceDto> placeDtos, MetaData metaData)> GetPlacesAsync(PlaceQueryString queryString, bool trackChanges);
        Task<PlaceDto> GetPlaceAsync(Guid placeId, bool trackChanges);
        Task<PlaceDto> CreatePlaceAsync(PlaceForCreationDto placeForCreationDto);
        Task UpdatePlaceAsync(Guid placeId, PlaceForUpdateDto placeForUpdateDto, bool trackChanges);
        Task DeletePlaceAsync(Guid placeId, bool trackChanges);
        Task<IEnumerable<PlaceDto>> GetNearestPlaces(double userLon, double userLat, bool trackChanges);
    }
}
