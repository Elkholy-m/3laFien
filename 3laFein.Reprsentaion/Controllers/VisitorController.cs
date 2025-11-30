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
    [Route("api/visitors")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public VisitorController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllVisitors()
        {
            var visitors = await _serviceManager.VisitorService.GetVisitorsAsync(false);
            return Ok(visitors);
        }

        [HttpGet("{visitorId:guid}", Name = "GetVisitorById")]
        public async Task<IActionResult> GetVisitor(Guid visitorId)
        {
            var visitor = await _serviceManager.VisitorService.GetVisitorAsync(visitorId, false);
            return Ok(visitor);
        }

        [HttpPost("{userId:guid}")]
        public async Task<IActionResult> CreateVisitor([FromRoute] Guid userId,
            [FromBody] VisitorForCreationDto visitorForCreationDto)
        {
            var visitor = await _serviceManager.VisitorService.CreateVisitorAsync(userId, visitorForCreationDto);
            return CreatedAtRoute("GetVisitorById", new {visitorId = visitor.VisitorId}, visitor);
        }

        [HttpPut("{visitorId:guid}")]
        public async Task<IActionResult> UpdateVisitor(Guid visitorId, VisitorForUpdateDto visitorForUpdateDto)
        {
            await _serviceManager.VisitorService.UpdateVisitorAsync(visitorId, visitorForUpdateDto, true);
            return NoContent();
        }

        [HttpDelete("{visitorId:guid}")]
        public async Task<IActionResult> DeleteVisitor(Guid visitorId)
        {
            await _serviceManager.VisitorService.DeleteVisitorAsync(visitorId, true);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.TryAdd("Allow", "GET, POST, PUT, DELETE, OPTIONS");
            return Ok();
        }

        [HttpPost("{visitorId:guid}/image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadVisitorImage([FromRoute]Guid visitorId, [FromForm] UploadImageDto uploadImageDto)
        {
            await _serviceManager.VisitorService.SetImageUrl(visitorId, uploadImageDto.File, _serviceManager.ImageService, true);
            return NoContent();
        }

        [HttpDelete("{visitorId:guid}/image")]
        public async Task<IActionResult> DeleteImage([FromRoute] Guid visitorId)
        {
            await _serviceManager.VisitorService.DeleteImage(visitorId, _serviceManager.ImageService, true);
            return NoContent();
        }
    }
}
