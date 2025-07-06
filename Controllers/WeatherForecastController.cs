using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FluentFoxApi.Controllers;

/// <summary>
/// Weather forecast controller for demonstration purposes
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Weather")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets a weather forecast for the next 5 days
    /// </summary>
    /// <param name="days">Number of days to forecast (1-10)</param>
    /// <returns>A list of weather forecasts</returns>
    /// <response code="200">Returns the weather forecast</response>
    /// <response code="400">If the days parameter is invalid</response>
    [HttpGet(Name = "GetWeatherForecast")]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<WeatherForecast>> Get([FromQuery, Range(1, 10)] int days = 5)
    {
        if (days < 1 || days > 10)
        {
            return BadRequest("Days must be between 1 and 10");
        }

        var forecasts = Enumerable.Range(1, days).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        _logger.LogInformation("Generated weather forecast for {Days} days", days);
        return Ok(forecasts);
    }

    /// <summary>
    /// Gets weather forecast for a specific date
    /// </summary>
    /// <param name="date">The date to get forecast for</param>
    /// <returns>Weather forecast for the specified date</returns>
    /// <response code="200">Returns the weather forecast for the date</response>
    /// <response code="400">If the date is in the past or too far in the future</response>
    [HttpGet("{date:datetime}", Name = "GetWeatherForecastByDate")]
    [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<WeatherForecast> GetByDate(DateTime date)
    {
        var today = DateTime.Today;
        if (date.Date <= today || date.Date > today.AddDays(30))
        {
            return BadRequest("Date must be tomorrow or within the next 30 days");
        }

        var forecast = new WeatherForecast
        {
            Date = DateOnly.FromDateTime(date),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        };

        _logger.LogInformation("Generated weather forecast for {Date}", date.ToString("yyyy-MM-dd"));
        return Ok(forecast);
    }
}
