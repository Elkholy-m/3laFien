using Microsoft.AspNetCore.Http;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IPlaceImageService
    {
        Task<IEnumerable<PlaceImageDto>> GetAllPlaceImages(Guid placeId, bool trackChanges);
        Task<PlaceImageDto> GetPlaceImage(Guid placeId, Guid imageId, bool trackChanges);
        Task<PlaceImageDto> GetMainImage(Guid placeId, bool trackChanges);
        Task<IEnumerable<PlaceImageDto>> CreatePlaceImages(Guid placeId, IFormFileCollection files, IImageService imageService);
        Task DeletePlaceImage(Guid placeId, Guid imageId, IImageService imageService, bool trackChanges);
        Task SetMainImage(Guid placeId, Guid mainImageId, bool trackChanges);
    }
}
