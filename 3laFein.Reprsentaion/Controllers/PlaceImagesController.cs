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
    [Route("api/places/{placeId:guid}/images")]
    [ApiController]
    public class PlaceImagesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public PlaceImagesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllImages([FromRoute] Guid placeId)
        {
            var images = await _serviceManager.PlaceImageService.GetAllPlaceImages(placeId, false);
            return Ok(images);
        }

        [HttpGet("{imageId:guid}")]
        public async Task<IActionResult> GetImage(Guid placeId, Guid imageId)
        {
            var image = await _serviceManager.PlaceImageService.GetPlaceImage(placeId, imageId, false);
            return Ok(image);
        }

        [HttpGet("main")]
        public async Task<IActionResult> GetMainImage(Guid placeId)
        {
            var mainImage = await _serviceManager.PlaceImageService.GetMainImage(placeId, false);
            return Ok(mainImage);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImages(Guid placeId, [FromForm] UploadImagesDto uploadImagesDto)
        {
            var placeImagesDto = await _serviceManager.PlaceImageService
                .CreatePlaceImages(placeId, uploadImagesDto.Files, _serviceManager.ImageService);
            return Ok(placeImagesDto);
        }

        [HttpPatch("{imageId:guid}/main")]
        public async Task<IActionResult> SetMainImage(Guid placeId, Guid imageId)
        {
            await _serviceManager.PlaceImageService.SetMainImage(placeId, imageId, true);
            return NoContent();
        }

        [HttpDelete("{imageId:guid}")]
        public async Task<IActionResult> DeleteImage(Guid placeId, Guid imageId)
        {
            await _serviceManager.PlaceImageService.DeletePlaceImage(placeId, imageId, _serviceManager.ImageService, true);
            return NoContent();
        }
    }
}
