using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite;
using Shared.DTO;

namespace Repository
{
    public class PlaceRepository(RepositoryContext context) : RepositoryBase<RepositoryContext, Place>(context), IPlaceRepository
    {
        public void CreatePlaceAsync(Place place) => Create(place);

        public void DeletePlace(Place place) => Delete(place);

        public Task<Place?> GetPlaceAsync(Guid placeId, bool trackChanges) =>
            FindByConditionWithIncludes(p => p.PlaceId.Equals(placeId) && p.IsDeleted == false, trackChanges, "PlaceImages,Reviews,PlaceSchedules")
                .SingleOrDefaultAsync();

        public async Task<PagedList<Place>> GetPlacesAsync(PlaceQueryString queryString, bool trackChanges)
        {
            // === Apply Filtering By Price Range And Min Rate ===
            var query = FindAllByConditionWithIncludes(
                    p => p.IsDeleted == false &&
                    queryString.MinPrice <= p.Price - (p.Price * p.DiscountPercentage / 100) &&
                    queryString.MaxPrice >= p.Price - (p.Price * p.DiscountPercentage / 100) &&
                    queryString.MinRate <= p.Rate,
                    trackChanges, "PlaceImages,Reviews,PlaceSchedules");

            // === Filter The Open Only Places ===
            if (queryString.OpenOnly.HasValue){
                var now = DateTime.UtcNow;
                var dbWeekDay = ((int)now.DayOfWeek + 1) % 7;
                var currentTime = now.TimeOfDay;

                // Filter Opened Places Only
                if (queryString.OpenOnly.Value) {
                    query = query.Where(place => place.PlaceSchedules!.Any(schedule =>
                                (int)schedule.WeekDay == dbWeekDay &&
                                currentTime > schedule.OpenTime &&
                                currentTime < schedule.ClosedTime));
                } // Filter Closed Places Only
                else {
                    query = query.Where(place => place.PlaceSchedules!.Any(schedule =>
                                (int)schedule.WeekDay != dbWeekDay ||
                                currentTime < schedule.OpenTime ||
                                currentTime > schedule.ClosedTime));

                }
            }

            // === Filter The Places That Have Discount ===
            if (queryString.DiscountOnly.HasValue && queryString.DiscountOnly == true) {
                query = query.Where(place => place.DiscountPercentage != 0);
            }

            // === Filter By Category ===
            if (queryString.CategoryId.HasValue) {
                query = query.Where(place => place.CategoryId == queryString.CategoryId.Value);
            }

            // === Filter By Country ===
            if (queryString.CountryId != null) {
                query = query.Where(place => place.CountryId == queryString.CountryId);
            }

            // === Filter By State ===
            if (queryString.StateId != null) {
                query = query.Where(place => place.StateId == queryString.StateId);
            }

            // === Filter By City ===
            if (queryString.CityId != null) {
                query = query.Where(place => place.CityId == queryString.CityId);
            }

            var count = await query.CountAsync();

            //=== (Ordering logic should be before the pagination) ===//

            // === Pagination Logic ===
            query = query
                .OrderBy(p => p.Name)
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
