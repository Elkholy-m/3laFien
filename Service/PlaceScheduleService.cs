using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PlaceScheduleService : IPlaceScheduleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PlaceScheduleService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlaceScheduleDto>> GetPlaceSchedules(Guid placeId, bool trackChanges)
        {
            await CheckPlaceExistance(placeId, trackChanges);
            var schedules = await _repositoryManager.PlaceSchedule.GetPlaceSchedulesAsync(placeId, trackChanges);
            return _mapper.Map<IEnumerable<PlaceScheduleDto>>(schedules);
        }

        public async Task<PlaceScheduleDto> GetPlaceSchedule(Guid placeId, Guid scheduleId, bool trackChanges)
        {
            await CheckPlaceExistance(placeId, trackChanges);
            PlaceSchedule schedule = await CheckScheduleExistance(placeId, scheduleId, trackChanges);
            return _mapper.Map<PlaceScheduleDto>(schedule);
        }


        public async Task<PlaceScheduleDto> CreatePlaceSchedule(Guid placeId, PlaceScheduleForCreationDto placeScheduleForCreationDto, bool trackChanges)
        {
            await CheckPlaceExistance(placeId, trackChanges);
            var schedule = _mapper.Map<PlaceSchedule>(placeScheduleForCreationDto);
            var scheduleInDb = await _repositoryManager.PlaceSchedule.GetPlaceScheduleByDayNumber(placeId, schedule.WeekDay, false);
            if (scheduleInDb is not null)
                throw new ScheduleAlreadyExistConflictException();
            _repositoryManager.PlaceSchedule.CreatePlaceSchedule(placeId, schedule);
            await _repositoryManager.SaveAsync();
            return _mapper.Map<PlaceScheduleDto>(schedule);
        }

        public async Task UpdatePlaceSchedule(Guid placeId, Guid scheduleId, PlaceScheduleForUpdateDto placeScheduleForUpdateDto, bool trackChanges)
        {
            await CheckPlaceExistance(placeId, trackChanges);
            PlaceSchedule schedule = await CheckScheduleExistance(placeId, scheduleId, trackChanges);
            _mapper.Map(placeScheduleForUpdateDto, schedule);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeletePlaceSchedule(Guid placeId, Guid scheduleId, bool trackChanges)
        {
            await CheckPlaceExistance(placeId, trackChanges);
            var schedule = await CheckScheduleExistance(placeId, scheduleId, trackChanges);
            _repositoryManager.PlaceSchedule.DeletePlaceSchedule(schedule);
            await _repositoryManager.SaveAsync();
        }

        private async Task CheckPlaceExistance(Guid placeId, bool trackChanges)
        {
            var place = await _repositoryManager.Place.GetPlaceAsync(placeId, trackChanges);
            if (place is null)
                throw new PlaceNotFoundException(placeId);
        }
        private async Task<PlaceSchedule> CheckScheduleExistance(Guid placeId, Guid scheduleId, bool trackChanges)
        {
            var schedule = await _repositoryManager.PlaceSchedule.GetPlaceScheduleAsync(placeId, scheduleId, trackChanges);
            if (schedule is null)
                throw new ScheduleNotFoundException(scheduleId);
            return schedule;
        }
    }
}
