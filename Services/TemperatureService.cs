using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using NETzadanie.Models;


namespace NETzadanie.Services
{
    public class TemperatureService : ITemperatureService
    {
        private readonly IWeatherService _weatherService;
        private readonly IDistributedCache _cache;

        private static readonly Dictionary<string, int> CityMapping =
            new(StringComparer.OrdinalIgnoreCase)
            {
            { "bratislava", 1 },
            { "praha", 2 },
            { "budapest", 3 },
            { "vieden", 4 }
            };

        public TemperatureService(
            IWeatherService weatherService,
            IDistributedCache cache)
        {
            _weatherService = weatherService;
            _cache = cache;
        }

        public async Task<TemperatureResponse?> GetTemperatureAsync(string city)
        {
            if (!CityMapping.TryGetValue(city, out var cityId))
                return null;

            var cacheKey = $"temperature:{city.ToLower()}";

            try
            {
                var weather = await _weatherService.GetTemperatureAsync(cityId);

                if (weather != null)
                {
                    var response = new TemperatureResponse
                    {
                        City = city.ToLower(),
                        TemperatureC = Math.Round((decimal)weather.TemperatureC, 2),
                        MeasuredAtUtc = weather.MeasuredAtUtc
                    };

                    await _cache.SetStringAsync(
                        cacheKey,
                        JsonSerializer.Serialize(response));

                    return response;
                }
            }
            catch
            {
                // fallback nižšie
            }

            // fallback na cache
            var cached = await _cache.GetStringAsync(cacheKey);

            if (cached != null)
                return JsonSerializer.Deserialize<TemperatureResponse>(cached);

            return null;
        }
    }
}
