using Microsoft.AspNetCore.Http;
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
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ImageController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpPost("visitors")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadVisitorImage([FromForm] UploadImageDto uploadImageDto)
        {
            var visitorImgResult = await _serviceManager.ImageService.VisitiorUploadAsync(uploadImageDto.File);
            return Ok(visitorImgResult);
        }

        [HttpPost("places")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPlaceImage([FromForm] UploadImageDto uploadImageDto)
        {
            var placeImgResult = await _serviceManager.ImageService.VisitiorUploadAsync(uploadImageDto.File);
            return Ok(placeImgResult);
        }
    }
}
