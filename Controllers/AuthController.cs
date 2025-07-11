using FluentFoxApi.Models;
using FluentFoxApi.Services;
using FluentFoxApi.Exceptions;
using FluentFoxApi.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace FluentFoxApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly JwtOptions _jwtOptions;
        private static readonly List<User> _users = new();
        private static int _nextId = 1;

        public AuthController(IAuthService authService, ILogger<AuthController> logger, IOptions<JwtOptions> jwtOptions)
        {
            _authService = authService;
            _logger = logger;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("register")]
        public ActionResult<ApiResponse<UserInfo>> Register([FromBody] RegisterRequest request)
        {
            _logger.LogInformation("Registering new user with email: {Email}", request.Email);

            if (_users.Any(u => u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ConflictException("A user with this email already exists");
            }

            var user = new User
            {
                Id = _nextId++,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Password = _authService.HashPassword(request.Password)
            };

            _users.Add(user);

            var userInfo = new UserInfo
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth
            };

            _logger.LogInformation("User registered successfully with ID: {UserId}", user.Id);
            return Ok(ApiResponse<UserInfo>.SuccessResponse(userInfo, "User registered successfully"));
        }

        [HttpPost("login")]
        public ActionResult<ApiResponse<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            _logger.LogInformation("User logging in with email: {Email}", request.Email);

            var user = _users.FirstOrDefault(u => u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase));

            if (user == null || !_authService.VerifyPassword(request.Password, user.Password))
            {
                throw new UnauthorizedException("Invalid email or password");
            }

            var token = _authService.GenerateJwtToken(user);
            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes);

            var userInfo = new UserInfo
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth
            };

            var response = new LoginResponse
            {
                Token = token,
                Expires = expires,
                User = userInfo
            };

            _logger.LogInformation("User logged in successfully with ID: {UserId}", user.Id);
            return Ok(ApiResponse<LoginResponse>.SuccessResponse(response, "User logged in successfully"));
        }
    }
}
