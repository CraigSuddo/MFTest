using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using UrlShortener.Dtos;
using UrlShortener.Logic.Storage;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DecodeController : ControllerBase
    {
        private readonly ILogger<DecodeController> _logger;
        private readonly IConfiguration _config;
        private readonly IStore _store;

        public DecodeController(ILogger<DecodeController> logger, IConfiguration config, IStore store)
        {
            _logger = logger;
            _store = store;
            _config = config;
        }

        [HttpPost]
        public UrlDto Decode(ShortenedUrlDto request)
        {
            var hostname = Regex.Escape(_config.GetValue<string>("ApplicationSettings:Hostname"));
            var regex = new Regex(@"(?!"+hostname+")([0-9a-zA-Z]{8,8})$");
            var matches = regex.Matches(request.ShortenedUrl);
            if (matches.Count == 0)
                return null;

            var match = matches[0];
            var code = match.Value;
            var existing = _store.GetByCode(code);
            if (existing == null)
                return null;

            return new UrlDto() { Url = existing.Url };
        }
    }
}
