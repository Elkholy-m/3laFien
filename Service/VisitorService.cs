using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class VisitorService : IVisitorService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public VisitorService(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _userManager = userManager;
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
            _repositoryManager.Visitor.CreateVisitorAsync(userId, visitor);
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

        private async Task<Visitor> CheckVisitorExistance(Guid visitorId, bool trackChanges)
        {
            var visitor = await _repositoryManager.Visitor.GetVisitorAsync(visitorId, trackChanges);
            if (visitor is null)
                throw new VisitorNotFoundException(visitorId);
            return visitor;
        }
    }
}
