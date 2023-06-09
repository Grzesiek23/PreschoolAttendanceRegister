﻿using System.Text.Json;
using FluentValidation;
using PAR.Application.Exceptions;
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
            case NotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                _logger.Debug(exception.Message);
                message = "Entity not found.";
                break;
            
            case BadRequestException:
                statusCode = StatusCodes.Status400BadRequest;
                message = JsonSerializer.Serialize(new {error = exception.Message});
                _logger.Error("{Message}", message);
                break;
            
            case InvalidCredentialsException:
                statusCode = StatusCodes.Status401Unauthorized;
                message = "Invalid credentials.";
                break;

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

            case InternalApplicationError:
                statusCode = StatusCodes.Status500InternalServerError;
                message = "Internal Server Error";
                var logMessage = JsonSerializer.Serialize(new {error = exception.Message});
                _logger.Error(exception, "{Message}", logMessage);
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