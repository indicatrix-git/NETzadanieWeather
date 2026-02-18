using NETzadanie.Models;

namespace NETzadanie.Services
{
    public interface ITemperatureService
    {
        Task<TemperatureResponse?> GetTemperatureAsync(string city);
    }
}
