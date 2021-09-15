using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using UrlShortener.Controllers;
using UrlShortener.Dtos;
using UrlShortener.Logic;
using Xunit;

namespace UrlShortener.Tests
{
    public class DecodingTests
    {
        [Fact]
        public void ShouldDecodeACodeToAURL()
        {
            // Arrange
            var testUrl = "http://www.musclefood.com";
            var request = new UrlDto() { Url = testUrl };
            var mockLoggerEncode = new Mock<ILogger<EncodeController>>();
            var mockLoggerDecode = new Mock<ILogger<DecodeController>>();
            var store = new Store();
            var config = Settings.GetSettings();

            var encodeController = new EncodeController(mockLoggerEncode.Object, config, store);
            var decodeController = new DecodeController(mockLoggerDecode.Object, config, store);
            var savedResult = encodeController.Encode(request);

            // Act
            var result = decodeController.Decode(new ShortenedUrlDto() { ShortenedUrl = savedResult.ShortenedUrl });

            // Assert
            Assert.NotEqual(0, result.Url.Length);
            Assert.Equal(testUrl, result.Url);
        }
    }
}
