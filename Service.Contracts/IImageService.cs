using Microsoft.AspNetCore.Http;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IImageService
    {
        Task<VisitorImageResult> VisitiorUploadAsync(IFormFile file);
        Task<PlaceImageResult> PlaceUploadAsync(IFormFile file);
        Task DeleteImageAsync(string imageUrl, string parentFolder);
    }
}
