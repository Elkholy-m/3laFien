using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IFavouritePlaceService
    {
        Task<IEnumerable<FavouritePlaceDto>> GetAllFavPlacesForVisitor(Guid visitorId, bool trackChanges);
        Task<FavouritePlaceDto> GetFavouritePlaceForVisitor(Guid visitorId, Guid placeId, bool trackChanges);
        Task<FavouritePlaceDto> CreateFavouritePlace(Guid visitorId, Guid placeId);
        Task DeleteFavouritePlace(Guid visitorId, Guid placeId, bool trackChanges);
    }
}
