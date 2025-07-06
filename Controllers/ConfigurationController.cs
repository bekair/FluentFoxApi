using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using FluentFoxApi.Configuration;

namespace FluentFoxApi.Controllers;

/// <summary>
/// Configuration controller for debugging and status information
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Configuration")]
public class ConfigurationController : ControllerBase
{
    private readonly ILogger<ConfigurationController> _logger;
    private readonly CorsOptions _corsOptions;

    public ConfigurationController(ILogger<ConfigurationController> logger, IOptions<CorsOptions> corsOptions)
    {
        _logger = logger;
        _corsOptions = corsOptions.Value;
    }

    /// <summary>
    /// Gets the current CORS configuration
    /// </summary>
    /// <returns>Current CORS settings</returns>
    /// <response code="200">Returns the CORS configuration</response>
    [HttpGet("cors")]
    [ProducesResponseType(typeof(CorsOptions), StatusCodes.Status200OK)]
    public ActionResult<CorsOptions> GetCorsConfiguration()
    {
        _logger.LogInformation("CORS configuration requested");
        return Ok(_corsOptions);
    }

    /// <summary>
    /// Gets basic API information
    /// </summary>
    /// <returns>API status and information</returns>
    /// <response code="200">Returns API information</response>
    [HttpGet("info")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<object> GetApiInfo()
    {
        var info = new
        {
            ApiName = "FluentFox API",
            Version = "v1.0.0",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
            ServerTime = DateTime.UtcNow,
            MachineName = Environment.MachineName,
            CorsEnabled = _corsOptions.AllowedOrigins.Length > 0
        };

        _logger.LogInformation("API info requested");
        return Ok(info);
    }
}
