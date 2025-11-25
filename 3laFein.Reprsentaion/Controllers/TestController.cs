using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3laFein.Reprsentaion.Controllers
{
    [AllowAnonymous]
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public TestController(ILoggerManager logger, UserManager<User> userManager, IEmailSender emailSender)
        {
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet("log")]
        public IActionResult TestLogging()
        {
            _logger.LogInfo("TestMessage.");
            _logger.LogWarn("TestMessage.");
            _logger.LogDebug("TestMessage.");
            _logger.LogError("TestMessage.");

            return Ok();
        }

        [HttpGet("email")]
        public async Task<IActionResult> TestEmailAsync()
        {
            var message = new Message(["ibnbatotoa@gmail.com"], "test", "Ibn Batota Applicaition.");
            var messageAsync = new Message(["ibnbatotoa@gmail.com"], "test-async", "Ibn Batota Applicaition Asynchronous.");
            _emailSender.SendEmail(message);
            await _emailSender.SendEmailAsync(messageAsync);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("user")]
        public async Task<IActionResult> AddDummyUser(dummyUser dummyUser)
        {
            var user = new User()
            {
                FirstName = dummyUser.FirstName,
                LastName = dummyUser.LastName,
                Email = dummyUser.Email,
                UserName = dummyUser.Email
            };
            var result = await _userManager.CreateAsync(user, dummyUser.Password!);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("error", error.Description);

                return BadRequest();
            }
            return Ok(user.Id);
        }
    }
}
