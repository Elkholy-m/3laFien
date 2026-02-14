using Contracts;
using Entities.PlaceDBModels;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ExternalCountryRepository(PlaceDbContext context) :
    RepositoryBase<PlaceDbContext, Country>(context),
    IExternalCountryRepository
{
    public async Task<IEnumerable<Country>> GetAllCountries(bool trackChanges) {
        return await FindAll(trackChanges).ToListAsync();
    }

    public async Task<Country?> GetCountryByCode(string code, bool trackChanges) {
        return await
            FindByCondition(c => c.Code == code, trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<Country?> GetCountryWithStates(int CountryId, bool trackChanges) {
        return await
            FindByConditionWithIncludes(c => c.Id == CountryId, trackChanges, "States")
            .SingleOrDefaultAsync();
    }
}
