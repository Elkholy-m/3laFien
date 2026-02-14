namespace Entities.Exceptions;

public sealed class LocationNotFoundException : NotFoundException
{
    public LocationNotFoundException(int countryId, int stateId, int cityId)
        : base($"Invalid Location With Country ID: {countryId} && State ID: {stateId} && City ID: {cityId}") { }
}
