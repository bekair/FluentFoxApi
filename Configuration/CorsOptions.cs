namespace FluentFoxApi.Configuration;

/// <summary>
/// Configuration options for CORS (Cross-Origin Resource Sharing)
/// </summary>
public class CorsOptions
{
    /// <summary>
    /// Configuration section name in appsettings.json
    /// </summary>
    public const string SectionName = "Cors";

    /// <summary>
    /// List of allowed origins for CORS requests
    /// </summary>
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Whether to allow credentials in CORS requests
    /// </summary>
    public bool AllowCredentials { get; set; } = true;

    /// <summary>
    /// Allowed headers for CORS requests (* for all)
    /// </summary>
    public string AllowedHeaders { get; set; } = "*";

    /// <summary>
    /// Allowed HTTP methods for CORS requests (* for all)
    /// </summary>
    public string AllowedMethods { get; set; } = "*";
}
