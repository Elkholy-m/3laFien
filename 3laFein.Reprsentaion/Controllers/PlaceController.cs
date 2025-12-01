using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3laFein.Reprsentaion.Controllers
{
    [Route("api/places")]
    [ApiController]
    [Authorize]
    public class PlaceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public PlaceController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllPlaces()
        {
            var places = await _serviceManager.PlaceService.GetPlacesAsync(false);
            return Ok(places);
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
