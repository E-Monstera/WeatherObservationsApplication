using Microsoft.AspNetCore.Mvc;
using WeatherObservationsApplication.Server.Interfaces;

namespace WeatherObservationsApplication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        /// <summary>
        /// Gets the Weather for a specific location
        /// </summary>
        /// <param name="location">The location provided by the user</param>
        /// <returns>The ServiceResponse object</returns>
        [HttpGet]
        [Route("{location}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] string location)
        {
            var res = await _weatherService.GetWeatherAsync(location);
            return Ok(res);
        }
    }
}
