using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPlaceImageRepository
    {
        Task<IEnumerable<PlaceImage>> GetPlaceImages(Guid placeId, bool trackChanges);
        Task<PlaceImage?> GetPlaceImage(Guid placeId, Guid imageId, bool trackChanges);
        Task<PlaceImage?> GetMainImage(Guid placeId, bool trackChanges);
        void CreatePlaceImage(Guid placeId, PlaceImage placeImage);
        void UpdatePlaceImage(PlaceImage placeImage);
        void DeletePlaceImage(PlaceImage placeImage);
        void SetMainImage(PlaceImage placeImage);
        void ResetMainImage(PlaceImage placeImage);
    }
}
