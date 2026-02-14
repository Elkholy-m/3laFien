using Contracts;
using Entities.PlaceDBModels;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ExternalCityRepository(PlaceDbContext context) :
    RepositoryBase<PlaceDbContext, City>(context),
    IExternalCityRepository
{
    public async Task<City?> GetCity(int countryId, int stateId, int CityId, bool trackChanges)
    {
        return await FindByCondition(city => city.CountryId == countryId &&
                city.StateId == stateId &&
                city.Id == CityId, trackChanges)
            .SingleOrDefaultAsync();
    }
}
