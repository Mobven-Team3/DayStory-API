using DayStory.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DayStory.WebAPI.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
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

        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = exception.Message,
            Type = exception.GetType().Name
        };
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        if (exception is BaseException)
        {
            var baseException = exception as BaseException;
            problemDetails.Status = (int)baseException.StatusCode;
            httpContext.Response.StatusCode = (int)baseException.StatusCode;
        }
        await httpContext.Response.WriteAsJsonAsync(problemDetails);
    }
}
