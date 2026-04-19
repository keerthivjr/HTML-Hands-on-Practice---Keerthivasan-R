using ContactManagement.API.Exceptions;
using ContactManagement.API.Models;
using System.Text.Json;

namespace ContactManagement.API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new ErrorResponse
            {
                Timestamp = DateTime.UtcNow
            };

            switch (exception)
            {
                case NotFoundException:
                    response.StatusCode = 404;
                    response.Message = exception.Message;
                    break;

                case BadRequestException:
                    response.StatusCode = 400;
                    response.Message = exception.Message;
                    break;

                case UnauthorizedException:
                    response.StatusCode = 401;
                    response.Message = exception.Message;
                    break;

                default:
                    response.StatusCode = 500;
                    response.Message = "Something went wrong. Please try again later.";
                    break;
            }

            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = "application/json";

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}