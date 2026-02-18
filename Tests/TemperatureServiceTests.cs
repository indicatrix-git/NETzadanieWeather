using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using NETzadanie.Models;
using NETzadanie.Services;
using System.Text.Json;
using Xunit;

namespace NETzadanie.Tests
{
    public class TemperatureServiceTests
    {
        [Fact]
        public async Task ReturnsCachedValue_WhenWeatherApiFails()
        {
            var weatherMock = new Mock<IWeatherService>();
            weatherMock.Setup(x => x.GetTemperatureAsync(It.IsAny<int>()))
                .ThrowsAsync(new HttpRequestException());

            var cache = new MemoryDistributedCache(
                Options.Create(new MemoryDistributedCacheOptions()));

            var cachedResponse = new TemperatureResponse
            {
                City = "bratislava",
                TemperatureC = 10.11M,
                MeasuredAtUtc = DateTime.UtcNow
            };

            await cache.SetStringAsync(
                "temperature:bratislava",
                JsonSerializer.Serialize(cachedResponse));

            var service = new TemperatureService(
                weatherMock.Object, cache);

            var result = await service.GetTemperatureAsync("bratislava");

            Assert.NotNull(result);
            Assert.Equal(10.11m, result.TemperatureC);
        }
    }
}
