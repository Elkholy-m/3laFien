using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IVisitorService
    {
        Task<IEnumerable<VisitorDto>> GetVisitorsAsync(bool trackChanges);
        Task<VisitorDto> GetVisitorAsync(Guid visitorId, bool trackChanges);
        Task<VisitorDto> CreateVisitorAsync(Guid userId, VisitorForCreationDto visitorForCreationDto);
        Task UpdateVisitorAsync(Guid visitorId, VisitorForUpdateDto visitorForUpdateDto, bool trackChanges);
        Task DeleteVisitorAsync(Guid visitorId, bool trackChanges);
    }
}
