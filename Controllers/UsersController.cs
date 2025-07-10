using Microsoft.AspNetCore.Mvc;
using FluentFoxApi.Models;
using FluentFoxApi.Exceptions;

namespace FluentFoxApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private static readonly List<User> _users = new();
        private static int _nextId = 1;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        public ActionResult<ApiResponse<List<User>>> GetUsers()
        {
            _logger.LogInformation("Getting all users");
            return Ok(ApiResponse<List<User>>.SuccessResponse(_users, "Users retrieved successfully"));
        }

        /// <summary>
        /// Gets a user by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User details</returns>
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<User>> GetUser(int id)
        {
            _logger.LogInformation("Getting user with ID: {UserId}", id);
            
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found");
            }

            return Ok(ApiResponse<User>.SuccessResponse(user, "User retrieved successfully"));
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="request">User creation data</param>
        /// <returns>Created user</returns>
        [HttpPost]
        public ActionResult<ApiResponse<User>> CreateUser([FromBody] CreateUserRequest request)
        {
            _logger.LogInformation("Creating new user with email: {Email}", request.Email);

            // Check if email already exists
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
                Password = HashPassword(request.Password) // In real app, use proper hashing
            };

            _users.Add(user);

            _logger.LogInformation("User created successfully with ID: {UserId}", user.Id);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, 
                ApiResponse<User>.SuccessResponse(user, "User created successfully"));
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="request">User update data</param>
        /// <returns>Updated user</returns>
        [HttpPut("{id}")]
        public ActionResult<ApiResponse<User>> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            _logger.LogInformation("Updating user with ID: {UserId}", id);

            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found");
            }

            // Check if email already exists for another user
            if (_users.Any(u => u.Id != id && u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ConflictException("A user with this email already exists");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.DateOfBirth = request.DateOfBirth;

            _logger.LogInformation("User updated successfully with ID: {UserId}", user.Id);
            return Ok(ApiResponse<User>.SuccessResponse(user, "User updated successfully"));
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Success response</returns>
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse> DeleteUser(int id)
        {
            _logger.LogInformation("Deleting user with ID: {UserId}", id);

            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found");
            }

            _users.Remove(user);

            _logger.LogInformation("User deleted successfully with ID: {UserId}", id);
            return Ok(ApiResponse.Success("User deleted successfully"));
        }

        private static string HashPassword(string password)
        {
            // In a real application, use BCrypt or similar
            return $"hashed_{password}";
        }
    }
}
