# FluentFox API - Swagger Documentation

## Overview

The FluentFox API now includes comprehensive Swagger/OpenAPI documentation for easy API exploration and testing.

## Accessing Swagger UI

When running the development server, Swagger UI is available at:
- **Swagger UI**: https://localhost:7093/swagger
- **OpenAPI JSON**: https://localhost:7093/swagger/v1/swagger.json

## Features Included

### 🔧 **Enhanced Configuration**
- Custom API information (title, version, description)
- Contact and license information
- JWT Bearer token authentication support
- CORS configuration for client integration

### 📚 **Rich Documentation**
- XML comments integration for detailed endpoint descriptions
- Request/response examples
- Parameter validation and constraints
- HTTP status code descriptions
- Data model documentation with examples

### 🔐 **Security**
- JWT Bearer token authentication scheme
- Authorization testing directly in Swagger UI
- Security requirements for protected endpoints

### 🌐 **CORS Support**
- Pre-configured for local development
- Allows requests from client applications (localhost:3000)

## API Endpoints

### Weather Forecast (Demo)
- `GET /api/WeatherForecast` - Get weather forecast for multiple days
- `GET /api/WeatherForecast/{date}` - Get forecast for specific date

### Configuration (Debug)
- `GET /api/Configuration/cors` - Get current CORS configuration
- `GET /api/Configuration/info` - Get API information and status

### Health Check
- `GET /health` - API health status

## Using Swagger UI

1. **Start the server** using any of the startup scripts
2. **Navigate to** https://localhost:7093/swagger
3. **Explore endpoints** by expanding the sections
4. **Test endpoints** using the "Try it out" button
5. **Authenticate** using the "Authorize" button for protected endpoints

## Development Features

- **Auto-reload** - Swagger updates automatically during development
- **Request duration** - See how long API calls take
- **Deep linking** - Share direct links to specific endpoints
- **Filtering** - Search through available endpoints
- **Validation** - Real-time request validation

## XML Documentation

The project is configured to generate XML documentation automatically, which powers the rich descriptions in Swagger UI. Add XML comments to your controllers and models for better documentation:

```csharp
/// <summary>
/// Gets user profile information
/// </summary>
/// <param name="userId">The unique identifier for the user</param>
/// <returns>User profile data</returns>
/// <response code="200">Returns the user profile</response>
/// <response code="404">If the user is not found</response>
[HttpGet("{userId}")]
[ProducesResponseType(typeof(UserProfile), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public ActionResult<UserProfile> GetProfile(int userId)
{
    // Implementation
}
```

## Next Steps

- Add authentication endpoints
- Create user management APIs
- Implement language learning specific endpoints
- Add file upload capabilities
- Configure production Swagger settings
