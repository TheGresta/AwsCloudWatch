using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherForecastController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("weather/{city}")]
        public async Task<IActionResult> GetForCity([FromRoute] string city, [FromQuery] int days = 5)
        {
            return Ok(await _weatherService.GetWeatherForCityAsync(city, days));
        }
    }
}