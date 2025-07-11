using System.Text.Json.Serialization;

namespace FluentFoxApi.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, string error)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = new List<string> { error }
            };
        }
    }

    // Non-generic version for cases where no data is returned
    public class ApiResponse : ApiResponse<object>
    {
        public static new ApiResponse Success(string message = "Operation successful")
        {
            return new ApiResponse
            {
                Message = message
            };
        }

        public static new ApiResponse ErrorResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse
            {
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }

        public static new ApiResponse ErrorResponse(string message, string error)
        {
            return new ApiResponse
            {
                Message = message,
                Errors = new List<string> { error }
            };
        }
    }
}
