using Entities.Models;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IPlaceScheduleService
    {
        Task<IEnumerable<PlaceScheduleDto>> GetPlaceSchedules(Guid placeId, bool trackChanges);
        Task<PlaceScheduleDto> GetPlaceSchedule(Guid placeId, Guid scheduleId, bool trackChanges);
        Task<PlaceScheduleDto> CreatePlaceSchedule(Guid placeId, PlaceScheduleForCreationDto placeScheduleForCreationDto, bool trackChanges);
        Task UpdatePlaceSchedule(Guid placeId, Guid scheduleId, PlaceScheduleForUpdateDto placeScheduleForUpdateDto, bool trackChanges);
        Task DeletePlaceSchedule(Guid placeId, Guid scheduleId, bool trackChanges);
    }
}
