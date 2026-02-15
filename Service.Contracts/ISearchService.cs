using Entities.Models;

namespace Service.Contracts;

public interface ISearchService
{
    Task IndexPlace(Place place);
    Task DeleteIndex(Guid placeId);
    Task<IReadOnlyList<Guid>> SearchPlaces(string searchTerm, int maxResults = 1000);
    Task RebuildIndex(IEnumerable<Place> places);
}
