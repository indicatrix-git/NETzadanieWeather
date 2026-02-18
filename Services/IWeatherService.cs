namespace NETzadanie.Services
{
    using NETzadanie.Models;

    public interface IWeatherService
    {
        Task<WeatherApiResponse?> GetTemperatureAsync(int cityId);
    }
}
