using System.Text.Json;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;

namespace Service
{
    public class PlaceService(IRepositoryManager repositoryManager,
            IMapper mapper, IHttpClientFactory httpClientFactory) : IPlaceService
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<IEnumerable<Place>> GetAllPlacesToRebuildIndex() =>
            await _repositoryManager.Place.GetAllPlacesToRebuildAsync();

        public async Task<PlaceDto> CreatePlaceAsync(PlaceForCreationDto placeForCreationDto, ISearchService searchService)
        {
            // Check Category Existance First
            await CheckCategoryExistance(placeForCreationDto);
            await CheckLocationExistance(placeForCreationDto);

            var place = _mapper.Map<Place>(placeForCreationDto);
            _repositoryManager.Place.CreatePlaceAsync(place);
            await _repositoryManager.SaveAsync();
            await searchService.IndexPlace(place);

            return _mapper.Map<PlaceDto>(place);
        }

        public async Task DeletePlaceAsync(Guid placeId, bool trackChanges, ISearchService searchService)
        {
            Place place = await CheckPlaceExistance(placeId, trackChanges);

            _repositoryManager.Place.DeletePlace(place);
            await _repositoryManager.SaveAsync();
            await searchService.DeleteIndex(placeId);
        }

        public async Task<PlaceDto> GetPlaceAsync(Guid placeId, bool trackChanges)
        {
            var place = await CheckPlaceExistance(placeId, trackChanges);
            PlaceDto placeDto = ManualMapEntity(place);
            return placeDto;
        }

        public async Task<(IEnumerable<PlaceDto> placeDtos, MetaData metaData)>
            GetPlacesAsync(PlaceQueryString queryString, bool trackChanges, ISearchService searchService)
        {
            IReadOnlyList<Guid>? ids = null;
            if (queryString.SearchTerm != null) {
                ids = await searchService.SearchPlaces(queryString.SearchTerm);
            }

            if (queryString.CountryId == null &&
                    queryString.UserLat != null && queryString.UserLon != null) {
                var httpClient = _httpClientFactory.CreateClient("Nominatim");
                var url = $"reverse?lat={queryString.UserLat}&lon={queryString.UserLon}&format=json";
                string response = await  httpClient.GetStringAsync(url);
                var countryCode = JsonDocument.Parse(response).RootElement
                    .GetProperty("address")
                    .GetProperty("country_code").GetString();

                var country = await _repositoryManager.CountryRepository
                    .GetCountryByCode(countryCode ?? string.Empty, false);

                queryString.CountryId = country?.Id;
            }

            PagedList<Place> pagedList = await _repositoryManager.Place.GetPlacesAsync(queryString, trackChanges, ids);
            List<PlaceDto> placesDto = ManualMapEntities(pagedList);
            return (placesDto, pagedList.MetaData);
        }

        public async Task<IEnumerable<PlaceDto>> GetNearestPlaces(double userLon, double userLat, bool trackChanges)
        {
            var places = await _repositoryManager.Place.GetPlacesNearestToUserAsync(userLon, userLat, trackChanges);
            var placesDto = ManualMapEntities(places);
            return placesDto;
        }

        public async Task<PlaceDto> UpdatePlaceAsync(Guid placeId, PlaceForUpdateDto placeForUpdateDto, bool trackChanges, ISearchService searchService)
        {
            // Check Category Existance First
            await CheckCategoryExistance(placeForUpdateDto);
            Place place = await CheckPlaceExistance(placeId, trackChanges);

            _mapper.Map(placeForUpdateDto, place);
            await _repositoryManager.SaveAsync();
            await searchService.IndexPlace(place);
            return _mapper.Map<PlaceDto>(place);
        }

        private async Task CheckCategoryExistance(PlaceForManipulation placeForManipulation)
        {
            var category = await _repositoryManager.Category.GetCategoryAsync(placeForManipulation.CategoryId, false);
            if (category is null)
                throw new CategoryNotFoundException(placeForManipulation.CategoryId);
        }

        private async Task CheckLocationExistance(PlaceForCreationDto placeForCreationDto) {
            var city = _repositoryManager
                .CityRepository
                .GetCity(placeForCreationDto.CountryId,
                        placeForCreationDto.StateId, placeForCreationDto.CityId, false);

            if (city == null) {
                throw new LocationNotFoundException(placeForCreationDto.CountryId,
                        placeForCreationDto.StateId,
                        placeForCreationDto.CityId);
            }
        }

        private async Task<Place> CheckPlaceExistance(Guid placeId, bool trackChanges)
        {
            var place = await _repositoryManager.Place.GetPlaceAsync(placeId, trackChanges);
            if (place is null)
                throw new PlaceNotFoundException(placeId);
            return place;
        }

        private PlaceDto ManualMapEntity(Place place)
        {
            var placeDto = _mapper.Map<PlaceDto>(place);

            // Manual Mapping
            placeDto.MainImageUrl = place.PlaceImages!.Where(img => img.IsMain).Select(img => img.ImageUrl).SingleOrDefault();
            if (place.Reviews is not null && place.Reviews.Any())
            {
                var reviewsCount = place.Reviews.Count();
                placeDto.TotalReviews = reviewsCount;
                placeDto.Rate = place.Reviews.Sum(rev => (float)rev.Rating) / reviewsCount;
            };
            var now = DateTime.UtcNow;
            var dbWeekDay = ((int)now.DayOfWeek + 1) % 7;
            var todayPlaceSchedule = place.PlaceSchedules!
                .SingleOrDefault(schedule => (int)schedule.WeekDay == dbWeekDay);
            if (todayPlaceSchedule is not null &&
                DateTime.UtcNow.TimeOfDay > todayPlaceSchedule.OpenTime &&
                DateTime.UtcNow.TimeOfDay < todayPlaceSchedule.ClosedTime)
                placeDto.IsOpened = true;
            return placeDto;
        }

        private List<PlaceDto> ManualMapEntities(IEnumerable<Place> places)
        {
            var placesDto = new List<PlaceDto>();
            foreach (var place in places)
            {
                var placeDto = ManualMapEntity(place);
                placesDto.Add(placeDto);
            }

            return placesDto;
        }
    }
}
