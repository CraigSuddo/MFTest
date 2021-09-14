using Microsoft.AspNetCore.Mvc;
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
        private readonly IStore _store;

        public DecodeController(ILogger<DecodeController> logger, IStore store)
        {
            _logger = logger;
            _store = store;
        }

        [HttpPost]
        public UrlDto Decode(ShortenedUrlDto request)
        {
            var regex = new Regex(@"(?!https:\/\/localhost:5001\/)([0-9a-zA-Z]{8,8})$");
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
