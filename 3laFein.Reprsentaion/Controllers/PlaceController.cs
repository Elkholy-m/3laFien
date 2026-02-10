using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;

namespace _3laFein.Reprsentaion.Controllers
{
    [Route("api/places")]
    [ApiController]
    public class PlaceController(IServiceManager serviceManager) : ControllerBase
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllPlaces([FromQuery] PlaceQueryString? queryString)
        {
            queryString ??= new PlaceQueryString();
            var (placeDtos, metaData) = await _serviceManager.PlaceService.GetPlacesAsync(queryString, false);
            Response.Headers.TryAdd("X-Pagination", JsonSerializer.Serialize(metaData));
            return Ok(placeDtos);
        }

        [HttpGet("nearest")]
        public async Task<IActionResult> GetAllPlacesByNearest(double userLon, double userLat)
        {
            var places = await _serviceManager.PlaceService.GetNearestPlaces(userLon, userLat, false);
            return Ok(places);
        }

        [HttpGet("{placeId:guid}", Name = "GetPlaceById")]
        public async Task<IActionResult> GetPlace(Guid placeId)
        {
            var place = await _serviceManager.PlaceService.GetPlaceAsync(placeId, false);
            return Ok(place);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlace([FromBody] PlaceForCreationDto placeForCreationDto)
        {
            var place = await _serviceManager.PlaceService.CreatePlaceAsync(placeForCreationDto);
            return CreatedAtRoute("GetPlaceById", new { placeId = place.PlaceId }, place);
        }

        [HttpPut("{placeId:guid}")]
        public async Task<IActionResult> UpdatePlace(Guid placeId, PlaceForUpdateDto placeForUpdateDto)
        {
            await _serviceManager.PlaceService.UpdatePlaceAsync(placeId, placeForUpdateDto, true);
            return NoContent();
        }

        [HttpDelete("{placeId:guid}")]
        public async Task<IActionResult> DeletePlace(Guid placeId)
        {
            await _serviceManager.PlaceService.DeletePlaceAsync(placeId, true);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.TryAdd("Allow", "GET, POST, PUT, DELETE, OPTIONS");
            return Ok();
        }
    }
}
