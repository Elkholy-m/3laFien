using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace _3laFein.Reprsentaion.Controllers
{
    [Route("api/places/{placeId:guid}/schedules")]
    [ApiController]
    public class placeScheduleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public placeScheduleController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllSchedulesForPlace([FromRoute] Guid placeId)
        {
            var schedules = await _serviceManager.PlaceScheduleService.GetPlaceSchedules(placeId, false);
            return Ok(schedules);
        }

        [HttpGet("{scheduleId:guid}", Name = "GetScheduleById")]
        public async Task<IActionResult> GetScheduleForPlace([FromRoute] Guid placeId, [FromRoute] Guid scheduleId)
        {
            var schedule = await _serviceManager.PlaceScheduleService.GetPlaceSchedule(placeId, scheduleId, false);
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> CreateScheduleForPlace([FromRoute] Guid placeId,
            [FromBody] PlaceScheduleForCreationDto placeScheduleForCreationDto)
        {
            if (placeScheduleForCreationDto is null)
                return BadRequest($"{nameof(PlaceScheduleForCreationDto)} can't be null.");
            var schedule = await _serviceManager.PlaceScheduleService.CreatePlaceSchedule(placeId, placeScheduleForCreationDto, false);
            return CreatedAtRoute("GetScheduleById", new { placeId, scheduleId = schedule.ScheduleId }, schedule);
        }

        [HttpPut("{scheduleId:guid}")]
        public async Task<IActionResult> UpdateScheduleForPlace([FromRoute] Guid placeId, [FromRoute] Guid scheduleId,
            [FromBody] PlaceScheduleForUpdateDto placeScheduleForUpdateDto)
        {
            if (placeScheduleForUpdateDto is null)
                return BadRequest($"{nameof(PlaceScheduleForUpdateDto)} can't be null.");
            await _serviceManager.PlaceScheduleService.UpdatePlaceSchedule(placeId, scheduleId, placeScheduleForUpdateDto, true);
            return NoContent();
        }

        [HttpDelete("{scheduleId:guid}")]
        public async Task<IActionResult> DeleteScheduleForPlace([FromRoute] Guid placeId, [FromRoute] Guid scheduleId)
        {
            await _serviceManager.PlaceScheduleService.DeletePlaceSchedule(placeId, scheduleId, true);
            return NoContent();
        }
    }
}
