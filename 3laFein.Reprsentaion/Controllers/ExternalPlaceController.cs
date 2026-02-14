using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace _3laFein.Reprsentaion.Controllers;

[Route("api/external/countries")]
[ApiController]
public class ExternalPlaceController(IRepositoryManager repositoryManager)
    : ControllerBase
{
    [HttpGet()]
    public async Task<IActionResult> GetAllCountries() {
        var countries = await  repositoryManager.CountryRepository.GetAllCountries(false);
        return Ok(countries);
    }

    [HttpGet("{id:int}/states")]
    public async Task<IActionResult> GetCountryWithStates([FromRoute] int id) {
        var country = await repositoryManager.CountryRepository.GetCountryWithStates(id, false);
        if (country == null) {
            return NotFound($@"Country with ID: {id} --> Didn't exist in the database");
        }
        return Ok(country);
    }

    [HttpGet("{id:int}/states/{stateId}/cities")]
    public async Task<IActionResult> StateWithCities([FromRoute] int id, [FromRoute] int stateId) {
        var state = await repositoryManager.StateRepository.GetAllCites(id, stateId, false);
        if (state == null) {
            return NotFound($@"Either
Country with ID: {id} OR
State with ID: {stateId} --> Didn't exist in the database");
        }
        return Ok(state);
    }


    [HttpGet("{id:int}/states/{stateId:int}/cities/{cityId:int}")]
    public async Task<IActionResult> StateWithCities([FromRoute] int id,
            [FromRoute] int stateId,
            [FromRoute] int cityId) {
        var city = await repositoryManager.CityRepository.GetCity(id, stateId, cityId, false);
        if (city == null) {
            return NotFound($@"Either
Country with ID: {id} OR
State with ID: {stateId} OR
City with ID: {cityId} --> Didn't exist in the database");
        }
        return Ok(city);
    }
}
