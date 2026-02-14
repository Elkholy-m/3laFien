using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FavouritePlaceRepository : RepositoryBase<RepositoryContext, FavouritePlaces>, IFavouritePlaceRepository
    {
        public FavouritePlaceRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<FavouritePlaces>> GetFavouritePlacesForUserAsync(Guid visitorId, bool trackChanges) => await
            FindByCondition(favPlace => favPlace.VisitorId.Equals(visitorId), trackChanges)
                .OrderBy(favPlace => favPlace.AddedAt)
                .ToListAsync();

        public async Task<FavouritePlaces?> GetFavouritePlaceForUserAsync(Guid visitorId, Guid placeId, bool trackChanges) => await
            FindByCondition(
                favPlace => favPlace.VisitorId.Equals(visitorId) &&
                favPlace.PlaceId.Equals(placeId),
            trackChanges)
            .SingleOrDefaultAsync();

        public void CreateFavouritePlace(FavouritePlaces favPlace) => Create(favPlace);

        public void DeleteFavouritePlaces(FavouritePlaces favPlace) => Delete(favPlace);
    }
}
