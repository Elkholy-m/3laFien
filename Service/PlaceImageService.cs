using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileNotFoundException = Entities.Exceptions.FileNotFoundException;

namespace Service
{
    public class PlaceImageService : IPlaceImageService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PlaceImageService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlaceImageDto>> GetAllPlaceImages(Guid placeId, bool trackChanges)
        {
            await CheckPlaceExistance(placeId, trackChanges);

            var placeImages = await _repositoryManager.PlaceImage.GetPlaceImages(placeId, trackChanges);
            return _mapper.Map<IEnumerable<PlaceImageDto>>(placeImages);
        }

        public async Task<PlaceImageDto> GetPlaceImage(Guid placeId, Guid imageId, bool trackChanges)
        {
            await CheckPlaceExistance(placeId, trackChanges);

            var placeImage = await CheckImageExistance(placeId, imageId, trackChanges);
            return _mapper.Map<PlaceImageDto>(placeImage);
        }

        public async Task<IEnumerable<PlaceImageDto>> CreatePlaceImages(Guid placeId, IFormFileCollection files, IImageService imageService)
        {
            await CheckPlaceExistance(placeId, false);

            // Upload images to the wwwroot
            var placeImageResults = await imageService.PlaceUploadAsync(files);


            // transform the placeImageResult into placeImage entity to save it into DB
            var placeImages = new List<PlaceImage>();
            foreach (var placeImageResult in placeImageResults)
                placeImages.Add(new PlaceImage { ImageUrl = Path.GetFileName(placeImageResult.FullUrl)});
            foreach (var placeImage in placeImages)
                _repositoryManager.PlaceImage.CreatePlaceImage(placeId, placeImage);


            // if there is no main image the first image to upload gone be the main image
            if (!(await _repositoryManager.PlaceImage.GetPlaceImages(placeId, false)).Any(img => img.IsMain))
                placeImages.FirstOrDefault()!.IsMain = true;

            await _repositoryManager.SaveAsync();
            return _mapper.Map<IEnumerable<PlaceImageDto>>(placeImages);
        }

        public async Task DeletePlaceImage(Guid placeId, Guid imageId, IImageService imageService, bool trackChanges)
        {
            await CheckPlaceExistance(placeId, trackChanges);

            var placeImage = await CheckImageExistance(placeId, imageId, trackChanges);
            await imageService.DeleteImageAsync(placeImage.ImageUrl!, "places");
            _repositoryManager.PlaceImage.DeletePlaceImage(placeImage);
            await _repositoryManager.SaveAsync();
        }

        public async Task<PlaceImageDto> GetMainImage(Guid placeId, bool trackChanges)
        {
            await CheckPlaceExistance(placeId, trackChanges);

            var mainPlaceImage = await _repositoryManager.PlaceImage.GetMainImage(placeId, trackChanges);
            if (mainPlaceImage is null)
                throw new FileNotFoundException($"The place with ID: {placeId} has no main image.");
            return _mapper.Map<PlaceImageDto>(mainPlaceImage);
        }

        public async Task SetMainImage(Guid placeId, Guid mainImageId, bool trackChanges)
        {
            await CheckPlaceExistance(placeId, trackChanges); 

            var images = await _repositoryManager.PlaceImage.GetPlaceImages(placeId, trackChanges);
            var oldMainImage = images.SingleOrDefault(x => x.IsMain);

            if (oldMainImage is not null)
            {
                // if the new main image equals the old one
                if (oldMainImage.ImageId.Equals(mainImageId))
                    return;

                _repositoryManager.PlaceImage.ResetMainImage(oldMainImage);
            }

            var placeImage = await CheckImageExistance(placeId, mainImageId, trackChanges);
            _repositoryManager.PlaceImage.SetMainImage(placeImage);
            await _repositoryManager.SaveAsync();
        }

        private async Task<PlaceImage> CheckImageExistance(Guid placeId, Guid imageId, bool trackChanges)
        {
            var placeImage = await _repositoryManager.PlaceImage.GetPlaceImage(placeId, imageId, trackChanges);
            if (placeImage is null)
                throw new FileNotFoundException($"Image with ID: {imageId} not exists in the database.");

            return placeImage;
        }

        private async Task CheckPlaceExistance(Guid placeId, bool trackChanges)
        {
            var place = await _repositoryManager.Place.GetPlaceAsync(placeId, trackChanges);
            if (place is null)
                throw new PlaceNotFoundException(placeId);
        }
    }
}
