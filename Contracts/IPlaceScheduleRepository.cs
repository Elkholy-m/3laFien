using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPlaceScheduleRepository
    {
        Task<IEnumerable<PlaceSchedule>> GetPlaceSchedulesAsync(Guid placeId, bool trackChanges);
        Task<PlaceSchedule?> GetPlaceScheduleAsync(Guid placeId, Guid scheduleId, bool trackChanges);
        void CreatePlaceSchedule(Guid placeId, PlaceSchedule schedule);
        void UpdatePlaceSchedule(PlaceSchedule schedule);
        void DeletePlaceSchedule(PlaceSchedule schedule);
    }
}
