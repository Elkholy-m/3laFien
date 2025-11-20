using Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3laFein.Reprsentaion.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public TestController(ILoggerManager logger) => _logger = logger;

        [HttpGet]
        public IActionResult TestLogging()
        {
            _logger.LogInfo("TestMessage.");
            _logger.LogWarn("TestMessage.");
            _logger.LogDebug("TestMessage.");
            _logger.LogError("TestMessage.");

            return Ok();
        }
    }
}
