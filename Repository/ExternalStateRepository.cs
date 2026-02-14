using Contracts;
using Entities.PlaceDBModels;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ExternalStateRepository(PlaceDbContext context) :
    RepositoryBase<PlaceDbContext, State>(context),
    IExternalStateRepository
{
    public async Task<State?> GetAllCites(int countryId, int stateId, bool trackChanges) {
        return await
            FindByConditionWithIncludes(st => st.CountryId == countryId && st.Id == stateId, trackChanges, "Cities")
            .SingleOrDefaultAsync();

    }
}
