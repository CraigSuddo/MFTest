using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using UrlShortener.Dtos;
using UrlShortener.Logic.Storage;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncodeController : ControllerBase
    {
        private readonly ILogger<EncodeController> _logger;
        private readonly IStore _store;
        private readonly IConfiguration _config;

        public EncodeController(ILogger<EncodeController> logger, IConfiguration config, IStore store)
        {
            _logger = logger;
            _store = store;
            _config = config;
        }

        [HttpPost]
        public ShortenedUrlDto Encode([FromBody] UrlDto request)
        {
            // Is the URL valid? This is a very simplistic regex I have written, it is by no means the real regex for checking a URL; I am just giving a rough idea!
            var regex = new Regex(@"(https|http)\:\/\/[0-9a-zA-Z\.]{1,}");
            var match = regex.Match(request.Url);
            if (!match.Success)
                return null;

            // Do we already have this request in the database?
            var existing = _store.GetByUrl(request.Url);
            if (existing != null)
                return GenerateDto(existing.Code);

            // Send the request to the storage object to store.
            var url = new ProcessedUrl { Url = request.Url, Code = GenerateCode() };
            _store.Save(url);

            return GenerateDto(url.Code);
        }

        /// <summary>
        /// Creates a DTO object from a string code.
        /// </summary>
        /// <param name="code">The code to put in the DTO.</param>
        /// <returns></returns>
        private ShortenedUrlDto GenerateDto(string code)
        {
            var hostname = _config.GetValue<string>("ApplicationSettings:Hostname");
            return new ShortenedUrlDto() { ShortenedUrl = $"{hostname}/{code}" };
        }

        /// <summary>
        /// Creates a new Short URL for the requested URL.
        /// </summary>
        /// <returns>A new short url code.</returns>
        private string GenerateCode()
        {
            var availableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var random = new Random();
            var output = new char[8];
            
            for (var i = 0; i < 8; i++) 
            {
                output[i] = availableChars[random.Next(availableChars.Length)];
            }

            return new string(output);
        }
    }
}
