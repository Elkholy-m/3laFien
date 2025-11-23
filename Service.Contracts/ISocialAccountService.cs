using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ISocialAccountService
    {
        Task<IEnumerable<SocialAccountDto>> GetSocialAccounts(Guid visitorId, bool trackChanges);
        Task<SocialAccountDto> GetSocialAccount(Guid visitorId, Guid accountId, bool trackChanges);
        Task<SocialAccountDto> CreateSocailAccount(Guid visitorId, SocialAccountForCreationDto accountForCreationDto, bool trackChanges);
        Task UpdateSocailAccount(Guid visitorId, Guid accountId, SocialAccountForUpdateDto accountForUpdateDto, bool trackChanges);
        Task DeleteSocialAccount(Guid visitorId, Guid accountId, bool trackChanges);
    }
}
