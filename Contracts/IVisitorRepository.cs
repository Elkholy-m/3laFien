using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVisitorRepository
    {
        Task<IEnumerable<Visitor>> GetVisitorsAsync(bool trackChanges);
        Task<Visitor?> GetVisitorAsync(Guid visitorId,  bool trackChanges);
        Task<Visitor?> GetVisitorByUserIdAsync(Guid userId, bool trackChanges);
        void CreateVisitor(Guid userId, Visitor visitor);
        void UpdateVisitor(Visitor visitor);
        void DeleteVisitor(Visitor visitor);
    }
}
