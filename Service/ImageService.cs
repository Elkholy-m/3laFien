using Entities.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders.Composite;
using Service.Contracts;
using Shared.DTO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class ImageService : IImageService
    {
        private readonly string _wwwroot;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private readonly string[] _allowedMimeTypes =
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/webp"
        };

        // Max image size (10 MB)
        private const int MaxSizeBytes = 10 * 1024 * 1024;

        public ImageService(IWebHostEnvironment webHost) => _wwwroot = webHost.WebRootPath;

        public async Task<VisitorImageResult> VisitiorUploadAsync(IFormFile file)
        {
            ValidateFile(file);
            ValidateSignature(file);

            using var image = Image.Load(file.OpenReadStream());

            Image profileImg = Resize(image, 300, 300);
            Image thumbnailImg = Resize(image, 100, 100);

            var photoId = Guid.NewGuid();

            string profileImgUrl = await SaveImageAsync(photoId, profileImg, "visitors/profile");
            string thumbnailImgUrl = await SaveImageAsync(photoId, thumbnailImg, "visitors/thumbnail");
            return new VisitorImageResult(profileImgUrl, thumbnailImgUrl);
        }
        public async Task<IEnumerable<PlaceImageResult>> PlaceUploadAsync(IEnumerable<IFormFile> files)
        {
            var placeImageResults = new List<PlaceImageResult>();
            foreach(var file in files)
            {
                ValidateFile(file);
                ValidateSignature(file);

                using var image = Image.Load(file.OpenReadStream());

                Image fullImg = Resize(image, 1200, 800);
                Image mediumImg = Resize(image, 800, 600);
                Image thumbnailImg = Resize(image, 300, 200);

                var photoId = Guid.NewGuid();

                string fullImgUrl = await SaveImageAsync(photoId, fullImg, "places/full");
                string mediumImgUrl = await SaveImageAsync(photoId, mediumImg, "places/medium");
                string thumbnailImgUrl = await SaveImageAsync(photoId, thumbnailImg, "places/thumbnail");
                placeImageResults.Add(new PlaceImageResult(fullImgUrl, mediumImgUrl, thumbnailImgUrl));
            }
            return placeImageResults;
        }

        public async Task DeleteImageAsync(string imageUrl, string parentFolder)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new InvalidFileBadRequestException("Invalid image url");

            var fileName = Path.GetFileName(imageUrl);

            if (string.IsNullOrWhiteSpace(fileName))
                throw new Entities.Exceptions.FileNotFoundException(imageUrl);

            if (parentFolder.Equals("visitors", StringComparison.InvariantCultureIgnoreCase))
            {
                var visitorPath = Path.Combine(_wwwroot, "visitors");
                foreach (var childFolder in new string[] { "thumbnail", "profile" })
                {
                    var childFolderPath = Path.Combine(visitorPath, childFolder, fileName);
                    if (File.Exists(childFolderPath))
                        File.Delete(childFolderPath);
                }
            }
            else if (parentFolder.Equals("places", StringComparison.InvariantCultureIgnoreCase))
            {
                var placePath = Path.Combine(_wwwroot, "places");
                foreach (var childFolder in new string[] { "full", "medium", "thumbnail" })
                {
                    var childFolderPath = Path.Combine(placePath, childFolder, fileName);
                    if (File.Exists(childFolderPath))
                        File.Delete(childFolderPath);
                }
            }
            await Task.CompletedTask;
        }


        private void ValidateFile(IFormFile file)
        {
            if (file is null || file.Length == 0)
                throw new InvalidFileBadRequestException("File is empty.");

            if (file.Length > MaxSizeBytes)
                throw new InvalidFileBadRequestException("File size exeeded 10 MB.");

            var ext = Path.GetExtension(file.FileName);
            if (!_allowedExtensions.Contains(ext.ToLower()))
                throw new InvalidFileBadRequestException("Invalid Extension.");

            if (!_allowedMimeTypes.Contains(file.ContentType))
                throw new InvalidFileBadRequestException("Invalid mime type.");
        }

        private void ValidateSignature(IFormFile file)
        {

            byte[] header = new byte[12];

            using var stream = file.OpenReadStream();
            stream.Read(header, 0, 12);

            // JPEG SOI
            if (header[0] == 0xFF && header[1] == 0xD8)
                return;

            // PNG signature
            byte[] png = { 137, 80, 78, 71 };
            if (header.Take(4).SequenceEqual(png))
                return;

            // gif sinsature
            byte[] gif = { 0x47, 0x49, 0x46, 0x38 };
            if (header.Take(4).SequenceEqual(gif))
                return;

            // WEBP = "RIFF....WEBP"
            if (Encoding.ASCII.GetString(header.Take(4).ToArray()) == "RIFF" &&
                Encoding.ASCII.GetString(header.Skip(8).Take(4).ToArray()) == "WEBP")
                return;

            throw new InvalidFileBadRequestException("Invalid image signature.");
        }

        private Image Resize(Image image, int width, int height)
        {
            return image.Clone(x => x.Resize(new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Crop
            }));
        }

        private async Task<string> SaveImageAsync(Guid photoId, Image profileImg, string folder)
        {
            var folderPath = Path.Combine(_wwwroot, folder);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{photoId}.jpg";
            var filePath = Path.Combine(folderPath, fileName);

            await profileImg.SaveAsJpegAsync(filePath);
            return $"/{folder}/{fileName}".Replace("\\", "/");
        }
    }
}
