using System.Net;
using System.Text.Json;
using NETzadanie.Models;


namespace NETzadanie.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherApiResponse?> GetTemperatureAsync(int cityId)
        {
            var response = await _httpClient.GetAsync(cityId.ToString());

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Weather API error: {response.StatusCode}");

            var stream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<WeatherApiResponse>(
                stream,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result;
        }
    }
}
