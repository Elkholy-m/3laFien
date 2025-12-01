using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Place>> GetPlacesAsync(bool trackChanges);
        Task<Place?> GetPlaceAsync(Guid placeId, bool trackChanges);
        void CreatePlaceAsync(Place place);
        void UpdatePlace(Place place);
        void DeletePlace(Place place);
        Task<List<Place>> GetPlacesNearestToUserAsync(double userLon, double userLat, bool trackChanges);
    }
}
