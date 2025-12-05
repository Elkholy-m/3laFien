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
    internal class FavouritePlaceService : IFavouritePlaceService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public FavouritePlaceService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FavouritePlaceDto>> GetAllFavPlacesForVisitor(Guid visitorId, bool trackChanges)
        {
            await CheckVisitorExistance(visitorId, trackChanges);
            var favPlaces = await _repositoryManager.favouritePlace.GetFavouritePlacesForUserAsync(visitorId, trackChanges);
            return _mapper.Map<IEnumerable<FavouritePlaceDto>>(favPlaces);
        }

        public async Task<FavouritePlaceDto> GetFavouritePlaceForVisitor(Guid visitorId, Guid placeId, bool trackChanges)
        {
            await CheckVisitorExistance(visitorId, trackChanges);
            await CheckPlaceExsitace(placeId, trackChanges);
            FavouritePlaces favPlace = await CheckFavPlaceExistance(visitorId, placeId, trackChanges);
            return _mapper.Map<FavouritePlaceDto>(favPlace);
        }

        public async Task<FavouritePlaceDto> CreateFavouritePlace(Guid visitorId, Guid placeId)
        {
            await CheckVisitorExistance(visitorId, false);
            await CheckPlaceExsitace(placeId, false);
            var favPlaceInDb = await _repositoryManager.favouritePlace.GetFavouritePlaceForUserAsync(visitorId, placeId, false);
            if (favPlaceInDb is not null)
                throw new PlaceAlreadyFavouritedConflictException();
            var favPlaceEntity = new FavouritePlaces { VisitorId = visitorId, PlaceId = placeId };
            _repositoryManager.favouritePlace.CreateFavouritePlace(favPlaceEntity);
            await _repositoryManager.SaveAsync();
            return _mapper.Map<FavouritePlaceDto>(favPlaceEntity);
        }

        public async Task DeleteFavouritePlace(Guid visitorId, Guid placeId, bool trackChanges)
        {
            await CheckVisitorExistance(visitorId, trackChanges);
            await CheckPlaceExsitace(placeId, trackChanges);
            var favPlace = await CheckFavPlaceExistance(visitorId, placeId, trackChanges);
            _repositoryManager.favouritePlace.DeleteFavouritePlaces(favPlace);
            await _repositoryManager.SaveAsync();
        }

        private async Task CheckVisitorExistance(Guid visitorId, bool trackChanges)
        {
            var visitor = await _repositoryManager.Visitor.GetVisitorAsync(visitorId, trackChanges);
            if (visitor is null)
                throw new VisitorNotFoundException(visitorId);
        }

        private async Task CheckPlaceExsitace(Guid placeId, bool trackChanges)
        {
            var place = await _repositoryManager.Place.GetPlaceAsync(placeId, trackChanges);
            if (place is null)
                throw new PlaceNotFoundException(placeId);
        }

        private async Task<FavouritePlaces> CheckFavPlaceExistance(Guid visitorId, Guid placeId, bool trackChanges)
        {
            var favPlace = await _repositoryManager.favouritePlace.GetFavouritePlaceForUserAsync(visitorId, placeId, trackChanges);
            if (favPlace is null)
                throw new FavouritePlaceNotFoundException(visitorId, placeId);
            return favPlace;
        }
    }
}
