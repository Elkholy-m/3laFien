using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PlaceScheduleRepository : RepositoryBase<PlaceSchedule>, IPlaceScheduleRepository
    {
        public PlaceScheduleRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<PlaceSchedule>> GetPlaceSchedulesAsync(Guid placeId, bool trackChanges) => await
            FindByCondition(schedule => schedule.PlaceId.Equals(placeId), trackChanges)
                .OrderBy(schedule => schedule.WeekDay)
                .ToListAsync();

        public async Task<PlaceSchedule?> GetPlaceScheduleAsync(Guid placeId, Guid scheduleId, bool trackChanges) => await
            FindByCondition(
                schedule => schedule.PlaceId.Equals(placeId) &&
                schedule.ScheduleId.Equals(scheduleId),
            trackChanges)
            .SingleOrDefaultAsync();

        public void CreatePlaceSchedule(Guid placeId, PlaceSchedule schedule)
        {
            schedule.PlaceId = placeId;
            Create(schedule);
        }

        public void UpdatePlaceSchedule(PlaceSchedule schedule) => Update(schedule);

        public void DeletePlaceSchedule(PlaceSchedule schedule) => Delete(schedule);

        public async Task<PlaceSchedule?> GetPlaceScheduleByDayNumber(Guid placeId, DayOfWeek dayOfWeek, bool trackChanges) => await
            FindByCondition(
                schedule => schedule.PlaceId.Equals(placeId) &&
                schedule.WeekDay.Equals(dayOfWeek),
            trackChanges)
            .SingleOrDefaultAsync();
    }
}
