namespace FluentFoxApi.Configuration;

/// <summary>
/// Configuration options for JWT (JSON Web Token) authentication
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Configuration section name in appsettings.json
    /// </summary>
    public const string SectionName = "Jwt";

    /// <summary>
    /// The secret key used to sign JWT tokens
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// The issuer of the JWT token
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// The audience of the JWT token
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Token expiration time in minutes
    /// </summary>
    public int ExpireMinutes { get; set; } = 60;

    /// <summary>
    /// Validates that all required JWT configuration values are present
    /// </summary>
    /// <returns>True if configuration is valid, false otherwise</returns>
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(Key) && 
               !string.IsNullOrEmpty(Issuer) && 
               !string.IsNullOrEmpty(Audience) && 
               ExpireMinutes > 0;
    }
} 