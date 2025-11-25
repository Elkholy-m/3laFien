using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3laFein.Reprsentaion.Controllers
{
    [Route("api/visitors/{visitorId:guid}/accounts")]
    [ApiController]
    public class SocialAccountsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public SocialAccountsController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetUserAccounts([FromRoute] Guid visitorId)
        {
            var accounts = await _serviceManager.socialAccountService.GetSocialAccounts(visitorId, false);
            return Ok(accounts);
        }

        [HttpGet("{accountId:guid}", Name = "GetAccountById")]
        public async Task<IActionResult> GetUserAccount([FromRoute] Guid visitorId, [FromRoute] Guid accountId)
        {
            var account = await _serviceManager.socialAccountService.GetSocialAccount(visitorId, accountId, false);
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountForVisitor([FromRoute] Guid visitorId,
            [FromBody] SocialAccountForCreationDto accountForCreationDto)
        {
            var account = await _serviceManager.socialAccountService.CreateSocailAccount(visitorId, accountForCreationDto, false);
            return CreatedAtRoute("GetAccountById", new { visitorId, accountId = account.AccountId }, account);
        }

        [HttpPut("{accountId:guid}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid visitorId, [FromRoute] Guid accountId,
            [FromBody] SocialAccountForUpdateDto accountForUpdateDto)
        {
            await _serviceManager.socialAccountService.UpdateSocailAccount(visitorId, accountId, accountForUpdateDto, true);
            return NoContent();
        }

        [HttpDelete("{accountId:guid}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] Guid visitorId, [FromRoute] Guid accountId)
        {
            await _serviceManager.socialAccountService.DeleteSocialAccount(visitorId, accountId, true);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.TryAdd("Allow", "GET, POST, PUT, DELETE, OPTIONS");
            return Ok();
        }
    }
}
