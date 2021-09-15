using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UrlShortener.Controllers;
using UrlShortener.Dtos;
using UrlShortener.Logic;
using Xunit;

namespace UrlShortener.Tests
{
    public class FollowTests
    {
        [Fact]
        public void ShouldDecodeACodeToAURLThenRedirect()
        {
            // Arrange
            var testUrl = "http://www.musclefood.com";
            var request = new UrlDto() { Url = testUrl };
            var mockLoggerEncode = new Mock<ILogger<EncodeController>>();
            var mockLoggerFollow = new Mock<ILogger<FollowController>>();
            var store = new Store();
            var settings = Settings.GetSettings();
            var encodeController = new EncodeController(mockLoggerEncode.Object, settings, store);
            var followController = new FollowController(mockLoggerFollow.Object, store);
            var savedResult = encodeController.Encode(request);

            // Act
            var code = savedResult.ShortenedUrl.Replace("https://localhost:5001/", "");
            var result = followController.Follow(code);

            // Assert
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal(testUrl, redirectResult.Url);
        }

        [Fact]
        public void ShouldFailAWrongCode()
        {
            // Arrange
            var testUrl = "http://www.musclefood.co.uk";
            var request = new UrlDto() { Url = testUrl };
            var mockLoggerEncode = new Mock<ILogger<EncodeController>>();
            var mockLoggerFollow = new Mock<ILogger<FollowController>>();
            var store = new Store();
            var settings = Settings.GetSettings();
            var encodeController = new EncodeController(mockLoggerEncode.Object, settings, store);
            var followController = new FollowController(mockLoggerFollow.Object, store);
            var savedResult = encodeController.Encode(request);

            // Act
            var result = followController.Follow("12345678");

            // Assert
            Assert.IsNotType<RedirectResult>(result);
        }
    }
}
