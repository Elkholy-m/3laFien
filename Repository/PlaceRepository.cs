using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite;
using Shared.DTO;
using Repository.Extensions;

namespace Repository
{
    public class PlaceRepository(RepositoryContext context) : RepositoryBase<RepositoryContext, Place>(context), IPlaceRepository
    {
        public async Task<IEnumerable<Place>> GetAllPlacesToRebuildAsync() {
            return await FindAll(false).ToListAsync();
        }
        public void CreatePlaceAsync(Place place) => Create(place);

        public void DeletePlace(Place place) => Delete(place);

        public Task<Place?> GetPlaceAsync(Guid placeId, bool trackChanges) =>
            FindByConditionWithIncludes(p => p.PlaceId.Equals(placeId) && p.IsDeleted == false, trackChanges, "PlaceImages,Reviews,PlaceSchedules")
                .SingleOrDefaultAsync();

        public async Task<PagedList<Place>> 
            GetPlacesAsync(PlaceQueryString queryString,
                    bool trackChanges, IEnumerable<Guid>? ids = null)
        {
            IQueryable<Place> query = FindByConditionWithIncludes(x => !x.IsDeleted, trackChanges,
                    "PlaceImages,Reviews,PlaceSchedules")
                .FilterByIds(queryString.SearchTerm, ids)
                .FilterByRange(queryString.MinPrice, queryString.MaxPrice, queryString.MinRate)
                .FilterByOpenOnly(queryString.OpenOnly)
                .FilterByDiscount(queryString.DiscountOnly)
                .FilterByCategory(queryString.CategoryId)
                .FilterByCountry(queryString.CountryId)
                .FilterByState(queryString.StateId)
                .FilterByCity(queryString.CityId)
                .Sort(queryString.OrderBy!);
                

            var count = await query.CountAsync();

            //=== (Ordering logic should be before the pagination) ===//

            // === Pagination Logic ===
            query = query
                .Skip((queryString.PageNumber - 1) * queryString.PageSize)
                .Take(queryString.PageSize);


            var pagedList = PagedList<Place>.ToPagedList(await query.ToListAsync(),
                    count,
                    queryString.PageSize,
                    queryString.PageNumber);

            return pagedList;
        }

        public async Task<List<Place>> GetPlacesNearestToUserAsync(double userLon, double userLat, bool trackChanges)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var userLocation = geometryFactory.CreatePoint(new Coordinate(userLon, userLat));

            // EF Core magic happens here
            return await FindByConditionWithIncludes(p => p.IsDeleted == false, trackChanges, "PlaceImages,Reviews,PlaceSchedules")
                // Calculate distance from user to every place and sort ascending
                .OrderBy(p => p.Location!.Distance(userLocation))
                .ToListAsync();
        }
        public void UpdatePlace(Place place) => Update(place);
    }
}
