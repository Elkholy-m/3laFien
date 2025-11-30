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
        Task<IEnumerable<PlaceImageResult>> PlaceUploadAsync(IEnumerable<IFormFile> files);
        Task DeleteImageAsync(string imageUrl, string parentFolder);
    }
}
