using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortener.Logic.Storage;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("{code}")]
    public class FollowController : ControllerBase
    {
        private readonly ILogger<FollowController> _logger;
        private readonly IStore _store;

        public FollowController(ILogger<FollowController> logger, IStore store)
        {
            _logger = logger;
            _store = store;
        }

        [HttpGet]
        public ActionResult Follow(string code)
        {
            var existing = _store.GetByCode(code);
            if (existing == null)
                return null;

            return new RedirectResult(existing.Url);
        }
    }
}
