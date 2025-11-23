using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISocialAccountRepository
    {
        Task<IEnumerable<SocialAccount>> GetSocialAccountsAsync(Guid visitorId, bool trackChanges);
        Task<SocialAccount?> GetSocialAccountAsync(Guid visitorId, Guid accountId, bool trackChanges);
        void CreateSocialAccount(Guid visitorId, SocialAccount account);
        void UpdateSocialAccount(SocialAccount account);
        void DeleteSocialAccount(SocialAccount account);
    }
}
