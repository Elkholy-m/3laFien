using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PlaceService : IPlaceService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;


        public PlaceService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<PlaceDto> CreatePlaceAsync(PlaceForCreationDto placeForCreationDto)
        {
            var place = _mapper.Map<Place>(placeForCreationDto);
            _repositoryManager.Place.CreatePlaceAsync(place);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<PlaceDto>(place);
        }

        public async Task DeletePlaceAsync(Guid placeId, bool trackChanges)
        {
            Place place = await CheckPlaceExistance(placeId, trackChanges);

            _repositoryManager.Place.DeletePlace(place);

            await _repositoryManager.SaveAsync();
        }

        public async Task<PlaceDto> GetPlaceAsync(Guid placeId, bool trackChanges)
        {
            var place = await CheckPlaceExistance(placeId, trackChanges);
            return _mapper.Map<PlaceDto>(place);
        }

        public async Task<IEnumerable<PlaceDto>> GetPlacesAsync(bool trackChanges)
        {
            var places = await _repositoryManager.Place.GetPlacesAsync(trackChanges);
            return _mapper.Map<IEnumerable<PlaceDto>>(places);
        }

        public async Task<IEnumerable<PlaceDto>> GetNearestPlaces(double userLon, double userLat, bool trackChanges)
        {
            var places = await _repositoryManager.Place.GetPlacesNearestToUserAsync(userLon, userLat, trackChanges);
            return _mapper.Map<IEnumerable<PlaceDto>>(places);
        }

        public async Task UpdatePlaceAsync(Guid placeId, PlaceForUpdateDto placeForUpdateDto, bool trackChanges)
        {
            Place place = await CheckPlaceExistance(placeId, trackChanges);

            _mapper.Map(placeForUpdateDto, place);
            await _repositoryManager.SaveAsync();
        }

        private async Task<Place> CheckPlaceExistance(Guid placeId, bool trackChanges)
        {
            var place = await _repositoryManager.Place.GetPlaceAsync(placeId, trackChanges);
            if (place is null)
                throw new PlaceNotFoundException(placeId);
            return place;
        }
    }
}
