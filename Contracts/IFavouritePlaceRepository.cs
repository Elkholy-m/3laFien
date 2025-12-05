using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFavouritePlaceRepository
    {
        Task<IEnumerable<FavouritePlaces>> GetFavouritePlacesForUserAsync(Guid visitorId, bool trackChanges);
        Task<FavouritePlaces?> GetFavouritePlaceForUserAsync(Guid visitorId, Guid placeId, bool trackChanges);
        void CreateFavouritePlace(FavouritePlaces favPlace);
        void DeleteFavouritePlaces(FavouritePlaces favPlace);
    }
}
