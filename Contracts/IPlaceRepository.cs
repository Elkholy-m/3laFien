using Entities.Models;
using Shared.DTO;

namespace Contracts
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Place>> GetAllPlacesToRebuildAsync();
        Task<PagedList<Place>> 
            GetPlacesAsync(PlaceQueryString queryString,
                    bool trackChanges, IEnumerable<Guid>? ids = null);

        Task<Place?> GetPlaceAsync(Guid placeId, bool trackChanges);
        void CreatePlaceAsync(Place place);
        void UpdatePlace(Place place);
        void DeletePlace(Place place);
        Task<List<Place>> GetPlacesNearestToUserAsync(double userLon, double userLat, bool trackChanges);
    }
}
