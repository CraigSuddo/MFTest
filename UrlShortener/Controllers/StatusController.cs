using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Obsolete("This class is in the downloaded project; not sure of it's purpose!")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "OK";
        }
    }
}
