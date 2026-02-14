using Entities.PlaceDBModels;

namespace Contracts;

public interface IExternalStateRepository {
    Task<State?> GetAllCites(int countryId, int stateId, bool trackChanges);
}
