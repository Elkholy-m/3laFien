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
    public class PlaceImageRepository : RepositoryBase<PlaceImage>, IPlaceImageRepository
    {
        public PlaceImageRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<PlaceImage>> GetPlaceImages(Guid placeId, bool trackChanges) => await
            FindByCondition(placeImg => placeImg.PlaceId.Equals(placeId), trackChanges)
                .OrderBy(placeImg => placeImg.ImageUrl)
                .ToListAsync();

        public async Task<PlaceImage?> GetPlaceImage(Guid placeId, Guid imageId, bool trackChanges) => await
                FindByCondition(
                    placeImg => placeImg.PlaceId.Equals(placeId) && placeImg.ImageId.Equals(imageId),
                    trackChanges
                )
                .SingleOrDefaultAsync();

        public async Task<PlaceImage?> GetMainImage(Guid placeId, bool trackChanges) => await
            FindByCondition(placeImage => placeImage.PlaceId.Equals(placeId) && placeImage.IsMain, trackChanges)
            .SingleOrDefaultAsync();

        public void CreatePlaceImage(Guid placeId, PlaceImage placeImage)
        {
            placeImage.PlaceId = placeId;
            Create(placeImage);
        }

        public void UpdatePlaceImage(PlaceImage placeImage) => Update(placeImage);

        public void DeletePlaceImage(PlaceImage placeImage) => Delete(placeImage);

        public void SetMainImage(PlaceImage placeImage) => placeImage.IsMain = true;

        public void ResetMainImage(PlaceImage placeImage) => placeImage.IsMain = false;

    }
}
