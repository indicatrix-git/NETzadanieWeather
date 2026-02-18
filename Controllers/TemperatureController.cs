using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETzadanie.Models;
using NETzadanie.Services;



namespace NETzadanie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureService _temperatureService;

        public TemperatureController(ITemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            var result = await _temperatureService.GetTemperatureAsync(city);

            if (result == null)
                return StatusCode(503, "No temperature data available.");

            return Ok(result);
        }



    }
}
