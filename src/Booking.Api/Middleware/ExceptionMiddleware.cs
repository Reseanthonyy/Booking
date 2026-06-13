using System.Net;
using System.Text.Json;
using Booking.Domain.Execptions;
using FluentValidation;

namespace Booking.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception ocurred.");

        var response = httpContext.Response;
        response.ContentType = "application/json";

        var statusCode = exception switch
        {
            ValidationException => (int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            DomainException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        response.StatusCode = statusCode;
        
        var result = JsonSerializer.Serialize(new
        {
            type = exception.GetType().Name,
            error = exception.Message,
            statusCode
        });

        await response.WriteAsync(result);
    }
}