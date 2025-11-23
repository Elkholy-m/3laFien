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
    internal class SocialAccountRepository : RepositoryBase<SocialAccount>, ISocialAccountRepository
    {
        public SocialAccountRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<SocialAccount>> GetSocialAccountsAsync(Guid visitorId, bool trackChanges) => await
                FindByCondition(account => account.VisitorId.Equals(visitorId), trackChanges)
                .OrderBy(account => account.Platform)
                .ToListAsync();

        public async Task<SocialAccount?> GetSocialAccountAsync(Guid visitorId, Guid accountId, bool trackChanges) => await
                FindByCondition(account => account.VisitorId.Equals(visitorId) && account.AccountId.Equals(accountId), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateSocialAccount(Guid visitorId, SocialAccount account)
        {
            account.VisitorId = visitorId;
            Create(account);
        }

        public void UpdateSocialAccount(SocialAccount account) => Update(account);

        public void DeleteSocialAccount(SocialAccount account) => Delete(account);

    }
}
