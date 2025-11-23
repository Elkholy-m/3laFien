using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IEmailSender _emailSender;

        public TestController(ILoggerManager logger, IEmailSender emailSender)
        {
            _logger = logger;
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
            var message = new Message(["ibrahimshabori@gmail.com"], "test", "Ibn Batota Applicaition.");
            var messageAsync = new Message(["ibnbatotoa@gmail.com"], "test-async", "Ibn Batota Applicaition Asynchronous.");
            _emailSender.SendEmail(message);
            await _emailSender.SendEmailAsync(messageAsync);
            return Ok();
        }
    }
}
