using Microsoft.AspNetCore.Http;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IPlaceService
    {
        Task<IEnumerable<PlaceDto>> GetPlacesAsync(bool trackChanges);
        Task<PlaceDto> GetPlaceAsync(Guid placeId, bool trackChanges);
        Task<PlaceDto> CreatePlaceAsync(PlaceForCreationDto placeForCreationDto);
        Task UpdatePlaceAsync(Guid placeId, PlaceForUpdateDto placeForUpdateDto, bool trackChanges);
        Task DeletePlaceAsync(Guid placeId, bool trackChanges);
        Task<IEnumerable<PlaceDto>> GetNearestPlaces(double userLon, double userLat, bool trackChanges);
    }
}
