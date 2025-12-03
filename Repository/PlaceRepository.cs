using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO;

namespace Repository
{
    public class PlaceRepository : RepositoryBase<Place>, IPlaceRepository
    {
        public PlaceRepository(RepositoryContext context) : base(context)
        {
        }
        public void CreatePlaceAsync(Place place) => Create(place);

        public void DeletePlace(Place place) => Delete(place);

        public Task<Place?> GetPlaceAsync(Guid placeId, bool trackChanges) =>
            FindByConditionWithIncludes(p => p.PlaceId.Equals(placeId) && p.IsDeleted == false, trackChanges, "PlaceImages,Reviews,PlaceSchedules")
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Place>> GetPlacesAsync(bool trackChanges) => await 
            FindAllByConditionWithIncludes(p => p.IsDeleted == false, trackChanges, "PlaceImages,Reviews,PlaceSchedules")
                 .OrderBy(p => p.Name)
                 .ToListAsync();

        public async Task<List<Place>> GetPlacesNearestToUserAsync(double userLon, double userLat, bool trackChanges)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var userLocation = geometryFactory.CreatePoint(new Coordinate(userLon, userLat));

            // EF Core magic happens here
            return await FindByConditionWithIncludes(p => p.IsDeleted == false, trackChanges, "PlaceImages,Reviews,PlaceSchedules")
                // Calculate distance from user to every place and sort ascending
                .OrderBy(p => p.Location.Distance(userLocation))
                .ToListAsync();
        }
        public void UpdatePlace(Place place) => Update(place);
    }
}
