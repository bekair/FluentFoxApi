# FluentFox Backend API

The backend API for the FluentFox language learning platform, built with .NET 8.

## Related Repositories

- **Frontend**: [FluentFoxClientApp](https://github.com/bekair/FluentFoxClientApp) - Next.js client application
- **Backend**: This repository - .NET Web API

## Project Structure

```
FluentFox/
├── FluentFox/              # .NET Web API project
│   ├── Controllers/        # API controllers
│   ├── Configuration/      # Configuration models
│   ├── Program.cs         # Application entry point
│   └── appsettings.json   # Application settings
├── start-backend.ps1      # Main startup script
├── start-backend.bat      # Batch file for easy startup
└── README.md             # This file
```

## Quick Start

### Prerequisites

- **.NET 8 SDK** or higher
- **Git**

### Running the API

#### Option 1: Using Startup Scripts (Recommended)
```bash
# Windows (Double-click or run)
start-backend.bat

# Or PowerShell
.\start-backend.ps1
```

#### Option 2: Manual Startup
```bash
cd FluentFox
dotnet restore
dotnet run
```

### API Endpoints

- **API Base URL**: https://localhost:7093
- **HTTP Endpoint**: http://localhost:5076
- **Swagger Documentation**: https://localhost:7093/swagger
- **Health Check**: https://localhost:7093/health
- **Configuration Info**: https://localhost:7093/api/Configuration/info

## API Documentation

The API includes comprehensive Swagger/OpenAPI documentation:

1. Start the API using the startup scripts
2. Navigate to https://localhost:7093/swagger
3. Explore and test endpoints interactively

### Available Endpoints

- **Weather Forecast** (Demo): `/api/WeatherForecast`
- **Configuration**: `/api/Configuration/*`
- **Health Check**: `/health`

## Configuration

### CORS Settings

CORS is configured via `appsettings.json` and `appsettings.Development.json`:

```json
{
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:3000",
      "https://localhost:3000"
    ],
    "AllowCredentials": true,
    "AllowedHeaders": "*",
    "AllowedMethods": "*"
  }
}
```

### Environment Configuration

- **Development**: Uses `appsettings.Development.json` (more permissive CORS)
- **Production**: Uses `appsettings.json` (restricted CORS)

## Development

See [Development.md](Development.md) for detailed development guidelines and coding standards.

## Tech Stack

- **Framework**: .NET 8
- **Language**: C#
- **API Documentation**: Swagger/OpenAPI 3.0
- **Authentication**: JWT Bearer (configured, ready for implementation)
- **CORS**: Configurable via appsettings
- **Database**: TBD

## Features

- 🔧 **RESTful API**: Comprehensive .NET Web API
- 📚 **Interactive Documentation**: Swagger UI with JWT auth support
- 🔐 **Security Ready**: CORS and JWT authentication configured
- 🚀 **Easy Development**: One-click startup scripts
- ⚙️ **Configuration-Based**: Settings via appsettings.json
- 🏥 **Health Checks**: Built-in health monitoring
- 🐛 **Debug Endpoints**: Configuration and status endpoints

## Connecting to Frontend

This API is designed to work with the [FluentFoxClientApp](https://github.com/bekair/FluentFoxClientApp) frontend.

1. Start this backend API (it will run on https://localhost:7093)
2. Start the frontend client (it will run on http://localhost:3000)
3. The frontend is pre-configured to connect to this API

## Contributing

1. Clone the repository
2. Create a feature branch
3. Make your changes
4. Test the API
5. Submit a pull request

## License

Private project - All rights reserved
