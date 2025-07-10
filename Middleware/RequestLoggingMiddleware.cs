using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FluentFoxApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var requestId = Guid.NewGuid().ToString();
            
            // Add request ID to response headers for tracing
            context.Response.Headers.Add("X-Request-ID", requestId);

            // Log request
            await LogRequest(context, requestId);

            // Capture response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();
                
                // Log response
                await LogResponse(context, requestId, stopwatch.ElapsedMilliseconds);

                // Copy response back to original stream
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task LogRequest(HttpContext context, string requestId)
        {
            var request = context.Request;
            
            var requestBody = string.Empty;
            if (request.ContentLength > 0 && IsLoggableContentType(request.ContentType))
            {
                request.EnableBuffering();
                using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            _logger.LogInformation(
                "Request {RequestId}: {Method} {Path} {QueryString} | Content-Type: {ContentType} | Body: {RequestBody}",
                requestId,
                request.Method,
                request.Path,
                request.QueryString,
                request.ContentType ?? "N/A",
                string.IsNullOrEmpty(requestBody) ? "N/A" : requestBody
            );
        }

        private async Task LogResponse(HttpContext context, string requestId, long elapsedMilliseconds)
        {
            var response = context.Response;
            
            var responseBody = string.Empty;
            if (response.Body.CanSeek && IsLoggableContentType(response.ContentType))
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                using var reader = new StreamReader(response.Body, Encoding.UTF8, leaveOpen: true);
                responseBody = await reader.ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
            }

            var logLevel = response.StatusCode >= 400 ? LogLevel.Warning : LogLevel.Information;
            
            _logger.Log(logLevel,
                "Response {RequestId}: {StatusCode} | Duration: {Duration}ms | Content-Type: {ContentType} | Body: {ResponseBody}",
                requestId,
                response.StatusCode,
                elapsedMilliseconds,
                response.ContentType ?? "N/A",
                string.IsNullOrEmpty(responseBody) ? "N/A" : responseBody
            );
        }

        private static bool IsLoggableContentType(string? contentType)
        {
            if (string.IsNullOrEmpty(contentType))
                return false;

            var loggableTypes = new[]
            {
                "application/json",
                "application/xml",
                "text/plain",
                "text/xml"
            };

            return loggableTypes.Any(type => contentType.Contains(type, StringComparison.OrdinalIgnoreCase));
        }
    }
}
