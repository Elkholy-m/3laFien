using Entities.PlaceDBModels;

namespace Contracts;

public interface IExternalCityRepository
{
    Task<City?> GetCity(int countryId, int stateId, int CityId, bool trackChanges);
}
