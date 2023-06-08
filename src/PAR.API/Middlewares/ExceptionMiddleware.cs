using System.Text.Json;
using FluentValidation;
using PAR.Contracts.Responses;
using Serilog;

namespace PAR.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger logger)
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
            await HandleExceptionAsync(ex, httpContext);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        int statusCode;
        string message;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = StatusCodes.Status400BadRequest;
                var validationFailureResponse = new ValidationFailureResponse
                {
                    Errors = validationException.Errors.Select(x => new ValidationResponse
                    {
                        PropertyName = x.PropertyName,
                        Message = x.ErrorMessage
                    })
                };
                message = JsonSerializer.Serialize(validationFailureResponse);
                _logger.Error(message);
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                message = JsonSerializer.Serialize(new {error = "Internal Server Error."});
                _logger.Error(exception, "Internal Server Error");
                break;
        }

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(message);
    }
}