using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class SocialAccountService : ISocialAccountService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public SocialAccountService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }


        private async Task CheckVisitorExistance(Guid visitorId, bool trackChanges)
        {
            var visitor = await _repositoryManager.Visitor.GetVisitorAsync(visitorId, trackChanges);
            if (visitor is null)
                throw new VisitorNotFoundException(visitorId);
        }

        private async Task<SocialAccount> CheckAccountExistance(Guid visitorId, Guid accountId, bool trackChanges)
        {
            var account = await _repositoryManager.SocialAccount.GetSocialAccountAsync(visitorId, accountId, trackChanges);
            if (account is null)
                throw new AccountNotFoundException(accountId);

            return account;
        }

        public async Task<IEnumerable<SocialAccountDto>> GetSocialAccounts(Guid visitorId, bool trackChanges)
        {
            await CheckVisitorExistance(visitorId, trackChanges);

            var accounts = await _repositoryManager.SocialAccount.GetSocialAccountsAsync(visitorId, trackChanges);
            return _mapper.Map<IEnumerable<SocialAccountDto>>(accounts);
        }

        public async Task<SocialAccountDto> GetSocialAccount(Guid visitorId, Guid accountId, bool trackChanges)
        {
            await CheckVisitorExistance(visitorId, trackChanges);
            var account = await CheckAccountExistance(visitorId, accountId, trackChanges);

            return _mapper.Map<SocialAccountDto>(account);
        }

        public async Task<SocialAccountDto> CreateSocailAccount(Guid visitorId, SocialAccountForCreationDto accountForCreationDto, bool trackChanges)
        {
            await CheckVisitorExistance(visitorId, trackChanges);

            var account = _mapper.Map<SocialAccount>(accountForCreationDto);
            _repositoryManager.SocialAccount.CreateSocialAccount(visitorId, account);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<SocialAccountDto>(account);
        }
        public async Task UpdateSocailAccount(Guid visitorId, Guid accountId, SocialAccountForUpdateDto accountForUpdateDto, bool trackChanges)
        {
            await CheckVisitorExistance(visitorId, trackChanges);
            var account = await CheckAccountExistance(visitorId, accountId, trackChanges);
            _mapper.Map(accountForUpdateDto, account);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteSocialAccount(Guid visitorId, Guid accountId, bool trackChanges)
        {
            await CheckVisitorExistance(visitorId, trackChanges);
            var account = await CheckAccountExistance(visitorId, accountId, trackChanges);
            _repositoryManager.SocialAccount.DeleteSocialAccount(account);
            await _repositoryManager.SaveAsync();
        }
    }
}
