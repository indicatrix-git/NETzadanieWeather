using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace NETzadanie.Tests
{
    public class TemperatureApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        TemperatureApiTests(
            WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Unauthorized_WithoutToken()
        {
            var response = await _client
                .GetAsync("/api/temperature/bratislava");

            Assert.Equal(HttpStatusCode.Unauthorized,
                response.StatusCode);
        }



    }
}
