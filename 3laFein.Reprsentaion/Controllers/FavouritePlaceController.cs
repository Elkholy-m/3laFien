using Microsoft.AspNetCore.Http.HttpResults;
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
    [Route("api/visitors/{visitorId:guid}/favourites")]
    [ApiController]
    public class FavouritePlaceController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public FavouritePlaceController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllFavourites([FromRoute] Guid visitorId)
        {
            var favPlaces = await _serviceManager.FavoritePlaceService.GetAllFavPlacesForVisitor(visitorId, false);
            return Ok(favPlaces);
        }

        [HttpGet("{placeId:guid}", Name = "GetFavPlaceById")]
        public async Task<IActionResult> GetFavouritePlace([FromRoute] Guid visitorId, [FromRoute] Guid placeId)
        {
            var favPlace = await _serviceManager.FavoritePlaceService.GetFavouritePlaceForVisitor(visitorId, placeId, false);
            return Ok(favPlace);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateFavouritPlace([FromRoute] Guid visitorId,
            [FromBody] FavouritePlaceForCreationDto favouritePlaceForCreationDto)
        {
            var favPlace = await _serviceManager.FavoritePlaceService.CreateFavouritePlace(visitorId, favouritePlaceForCreationDto.PlaceId);
            return CreatedAtRoute("GetFavPlaceById", new { visitorId, placeId = favPlace.PlaceId }, favPlace);
        }

        [HttpDelete("{placeId:guid}")]
        public async Task<IActionResult> DeleteFavouritPlace([FromRoute] Guid visitorId, [FromRoute] Guid placeId)
        {
            await _serviceManager.FavoritePlaceService.DeleteFavouritePlace(visitorId, placeId, true);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.TryAdd("Allow", "GET, POST, DELETE, OPTIONS");
            return Ok();
        }
    }
}
