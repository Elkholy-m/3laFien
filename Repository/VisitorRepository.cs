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
    internal class VisitorRepository :RepositoryBase<Visitor>, IVisitorRepository
    {
        public VisitorRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<Visitor>> GetVisitorsAsync(bool trackChanges) => await
                FindByCondition(visitor => !visitor.IsDeleted, trackChanges)
                .ToListAsync();

        public async Task<Visitor?> GetVisitorAsync(Guid visitorId, bool trackChanges) => await
                FindByCondition(visitor => visitor.VisitorId.Equals(visitorId) && !visitor.IsDeleted, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateVisitorAsync(Guid userId, Visitor visitor)
        {
            visitor.UserId = userId;
            Create(visitor);
        }

        public void UpdateVisitor(Visitor visitor) => Update(visitor);

        public void DeleteVisitor(Visitor visitor) => Delete(visitor);
    }
}
