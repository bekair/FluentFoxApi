using System.ComponentModel.DataAnnotations;

namespace FluentFoxApi;

/// <summary>
/// Represents a weather forecast for a specific date
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// The date of the forecast
    /// </summary>
    /// <example>2024-01-15</example>
    [Required]
    public DateOnly Date { get; set; }

    /// <summary>
    /// Temperature in Celsius
    /// </summary>
    /// <example>25</example>
    [Range(-50, 60)]
    public int TemperatureC { get; set; }

    /// <summary>
    /// Temperature in Fahrenheit (calculated from Celsius)
    /// </summary>
    /// <example>77</example>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Brief description of the weather conditions
    /// </summary>
    /// <example>Sunny</example>
    [StringLength(50)]
    public string? Summary { get; set; }
}
