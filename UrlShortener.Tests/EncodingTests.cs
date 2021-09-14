using Microsoft.Extensions.Logging;
using Moq;
using UrlShortener.Controllers;
using UrlShortener.Dtos;
using UrlShortener.Logic;
using UrlShortener.Logic.Storage;
using Xunit;

namespace UrlShortener.Tests
{
    public class EncodingTests
    {
        private const string ExpectedUrlRegex = @"^https:\/\/localhost:5001\/[0-9a-zA-Z]{8,8}$";

        [Fact]
        public void ShouldReturnAShortURLWhenGivenAURL()
        {
            // Arrange
            var request = new UrlDto() { Url = "http://www.google.com" };
            var mockLogger = new Mock<ILogger<EncodeController>>();
            var mockStore = new Mock<IStore>();
            var controller = new EncodeController(mockLogger.Object, mockStore.Object);

            // Act
            var result = controller.Encode(request);

            // Assert
            Assert.NotEqual(0, result.ShortenedUrl.Length);
            Assert.Matches(ExpectedUrlRegex, result.ShortenedUrl);
        }

        [Fact]
        public void ShouldGetDifferentResultsWhenRanTwiceForDifferentURLs()
        {
            // Arrange
            var requestA = new UrlDto() { Url = "http://www.google.com" };
            var requestB = new UrlDto() { Url = "http://www.musclefood.co.uk" };
            var mockLogger = new Mock<ILogger<EncodeController>>();
            var mockStore = new Mock<IStore>();
            var controller = new EncodeController(mockLogger.Object, mockStore.Object);

            // Act
            var resultA = controller.Encode(requestA);
            var resultB = controller.Encode(requestB);

            // Assert
            Assert.Matches(ExpectedUrlRegex, resultA.ShortenedUrl);
            Assert.Matches(ExpectedUrlRegex, resultB.ShortenedUrl);
            Assert.NotEqual(resultA.ShortenedUrl, resultB.ShortenedUrl);
        }

        [Fact]
        public void ShouldGetTheSameShortURLForTheSameRequestTwice()
        {
            // Arrange
            var requestA = new UrlDto() { Url = "http://www.musclefood.co.uk" };
            var requestB = new UrlDto() { Url = "http://www.musclefood.co.uk" };
            var mockLogger = new Mock<ILogger<EncodeController>>();
            var store = new Store();
            var controller = new EncodeController(mockLogger.Object, store);

            // Act
            var resultA = controller.Encode(requestA);
            var resultB = controller.Encode(requestB);

            // Assert
            Assert.NotEqual(0, resultA.ShortenedUrl.Length);
            Assert.NotEqual(0, resultB.ShortenedUrl.Length);
            Assert.Matches(ExpectedUrlRegex, resultA.ShortenedUrl);
            Assert.Matches(ExpectedUrlRegex, resultB.ShortenedUrl);
            Assert.Equal(resultA.ShortenedUrl, resultB.ShortenedUrl);
        }

        [Fact]
        public void ShouldNotEncodeABadUrl()
        {
            // Arrange
            var request = new UrlDto() { Url = "wrongurl" };
            var mockLogger = new Mock<ILogger<EncodeController>>();
            var store = new Store();
            var controller = new EncodeController(mockLogger.Object, store);

            // Act
            var result = controller.Encode(request);

            // Assert
            Assert.Null(result);
        }
    }
}
