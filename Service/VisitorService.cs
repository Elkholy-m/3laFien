using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Service.Contracts;
using Shared.DTO;
using SixLabors.ImageSharp.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileNotFoundException = Entities.Exceptions.FileNotFoundException;

namespace Service
{
    internal class VisitorService : IVisitorService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly string _wwwroot;

        public VisitorService(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager, IWebHostEnvironment webHost)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _userManager = userManager;
            _wwwroot = webHost.WebRootPath;
        }

        public async Task<IEnumerable<VisitorDto>> GetVisitorsAsync(bool trackChanges)
        {
            var visitors = await _repositoryManager.Visitor.GetVisitorsAsync(trackChanges);
            return _mapper.Map<IEnumerable<VisitorDto>>(visitors);
        }

        public async Task<VisitorDto> GetVisitorAsync(Guid visitorId, bool trackChanges)
        {
            Visitor visitor = await CheckVisitorExistance(visitorId, trackChanges);
            return _mapper.Map<VisitorDto>(visitor);
        }

        public async Task<VisitorDto> CreateVisitorAsync(Guid userId, VisitorForCreationDto visitorForCreationDto)
        {
            var visitor = _mapper.Map<Visitor>(visitorForCreationDto);
            _repositoryManager.Visitor.CreateVisitor(userId, visitor);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<VisitorDto>(visitor);
        }

        public async Task UpdateVisitorAsync(Guid visitorId, VisitorForUpdateDto visitorForUpdateDto, bool trackChanges)
        {
            Visitor visitor = await CheckVisitorExistance(visitorId, trackChanges);

            _mapper.Map(visitorForUpdateDto, visitor);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteVisitorAsync(Guid visitorId, bool trackChanges)
        {
            Visitor visitor = await CheckVisitorExistance(visitorId, trackChanges);

            // Get user for soft delete
            var user = await _userManager.Users.Where(user => user.Id.Equals(visitor.UserId) && !user.IsDeleted).SingleOrDefaultAsync();
            if (user is null)
                throw new UserNotFoundException(visitor.UserId);

            _repositoryManager.Visitor.DeleteVisitor(visitor);
            await _userManager.DeleteAsync(user);

            await _repositoryManager.SaveAsync();
        }

        public async Task SetImageUrl(Guid visitorId, IFormFile file, IImageService imageService, bool trackChanges)
        {
            var visitor = await CheckVisitorExistance(visitorId, trackChanges);
            if (visitor.ImageUrl is not null)
                throw new InvalidFileBadRequestException("The visitor already has an image delete it first then upload.");

            var imageResult = await imageService.VisitiorUploadAsync(file);

            visitor.ImageUrl = Path.GetFileName(imageResult.ThumbnailUrl);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteImage(Guid visitorId, IImageService imgService, bool trackChanges)
        {
            var visitor = await CheckVisitorExistance(visitorId, true);
            await imgService.DeleteImageAsync(visitor.ImageUrl!, "visitors");
            visitor.ImageUrl = null;
            await _repositoryManager.SaveAsync();
        }

        private async Task<Visitor> CheckVisitorExistance(Guid visitorId, bool trackChanges)
        {
            var visitor = await _repositoryManager.Visitor.GetVisitorAsync(visitorId, trackChanges);
            if (visitor is null)
                throw new VisitorNotFoundException(visitorId);
            return visitor;
        }
    }
}
