namespace FluentFoxApi.Exceptions
{
    public class BaseApiException : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }

        public BaseApiException(int statusCode, string errorCode, string message) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public BaseApiException(int statusCode, string errorCode, string message, Exception innerException) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }

    public class BadRequestException : BaseApiException
    {
        public BadRequestException(string message) 
            : base(400, "BAD_REQUEST", message)
        {
        }

        public BadRequestException(string message, Exception innerException) 
            : base(400, "BAD_REQUEST", message, innerException)
        {
        }
    }

    public class UnauthorizedException : BaseApiException
    {
        public UnauthorizedException(string message = "Unauthorized access") 
            : base(401, "UNAUTHORIZED", message)
        {
        }

        public UnauthorizedException(string message, Exception innerException) 
            : base(401, "UNAUTHORIZED", message, innerException)
        {
        }
    }

    public class ForbiddenException : BaseApiException
    {
        public ForbiddenException(string message = "Access forbidden") 
            : base(403, "FORBIDDEN", message)
        {
        }

        public ForbiddenException(string message, Exception innerException) 
            : base(403, "FORBIDDEN", message, innerException)
        {
        }
    }

    public class NotFoundException : BaseApiException
    {
        public NotFoundException(string message = "Resource not found") 
            : base(404, "NOT_FOUND", message)
        {
        }

        public NotFoundException(string message, Exception innerException) 
            : base(404, "NOT_FOUND", message, innerException)
        {
        }
    }

    public class ConflictException : BaseApiException
    {
        public ConflictException(string message = "Resource conflict") 
            : base(409, "CONFLICT", message)
        {
        }

        public ConflictException(string message, Exception innerException) 
            : base(409, "CONFLICT", message, innerException)
        {
        }
    }

    public class ValidationException : BaseApiException
    {
        public List<string> ValidationErrors { get; }

        public ValidationException(List<string> validationErrors) 
            : base(422, "VALIDATION_ERROR", "Validation failed")
        {
            ValidationErrors = validationErrors;
        }

        public ValidationException(string validationError) 
            : base(422, "VALIDATION_ERROR", "Validation failed")
        {
            ValidationErrors = new List<string> { validationError };
        }
    }

    public class InternalServerException : BaseApiException
    {
        public InternalServerException(string message = "An internal server error occurred") 
            : base(500, "INTERNAL_SERVER_ERROR", message)
        {
        }

        public InternalServerException(string message, Exception innerException) 
            : base(500, "INTERNAL_SERVER_ERROR", message, innerException)
        {
        }
    }
}
