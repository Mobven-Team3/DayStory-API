using DayStory.WebAPI.Helpers;
using Serilog;
using System.Text;

namespace DayStory.WebAPI.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userId = JwtHelper.GetUserIdFromToken(context);

        await LogRequestBody(context, userId);

        var originalResponseBody = context.Response.Body;

        using (var memoryStream = new MemoryStream())
        {
            context.Response.Body = memoryStream;

            try
            {
                await _next(context);

                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(originalResponseBody);
                context.Response.Body = originalResponseBody;

                memoryStream.Seek(0, SeekOrigin.Begin);
                await LogResponseBody(memoryStream);
            }
            finally
            {
                memoryStream.Dispose();
            }
        }
    }

    private async Task LogRequestBody(HttpContext context, string userId)
    {
        if (context.Request.ContentLength.HasValue && context.Request.ContentLength >= 0)
        {
            context.Request.EnableBuffering();
            string requestBody;

            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            if (!string.IsNullOrWhiteSpace(requestBody))
            {
                Log.Information($"User ID: {userId} - Request Body: {requestBody}");
            }
        }
    }

    private async Task LogResponseBody(Stream responseBodyStream)
    {
        if (responseBodyStream.CanRead && responseBodyStream.Length > 0)
        {
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(responseBodyStream))
            {
                var responseBody = await reader.ReadToEndAsync();
                Log.Information($"Response Body: {responseBody}");
            }
        }
    }
}
