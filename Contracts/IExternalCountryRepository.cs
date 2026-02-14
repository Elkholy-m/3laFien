using Entities.PlaceDBModels;

namespace Contracts;

public interface IExternalCountryRepository
{
    Task<IEnumerable<Country>> GetAllCountries(bool trackChanges);
    Task<Country?> GetCountryWithStates(int CountryId, bool trackChanges);
    Task<Country?> GetCountryByCode(string code, bool trackChanges);
}
