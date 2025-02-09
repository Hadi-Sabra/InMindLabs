namespace Lab1.Middlewares;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            
            var requestInfo = await FormatRequest(context.Request);
            
            // Logging basic request information
            _logger.LogInformation($"UTC Timestamp: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} \n" +
                                   $"Method: {context.Request.Method} \n" +
                                   $"Path: {context.Request.Path} \n" +
                                   $"QueryString: {context.Request.QueryString}");
            
            //Logging Headers + Request body if applicable
            _logger.LogInformation("Request Information: {RequestInfo}", requestInfo);

            

            var originalBodyStream = context.Response.Body;// The real stream where ASP.NET Core writes the response to send to the client.
            using var responseBody = new MemoryStream(); //A temporary in-memory buffer to capture the response for logging.
            context.Response.Body = responseBody;

            
            var startTime = DateTime.UtcNow;
            
            await _next(context);
            
            var responseInfo = await FormatResponse(context.Response);
            var duration = DateTime.UtcNow - startTime;

            _logger.LogInformation(
                "Response Information: Status: {StatusCode}, Duration: {Duration}ms, Body: {ResponseInfo}",
                context.Response.StatusCode,
                duration.TotalMilliseconds,
                responseInfo);

            // Copying the response to the original stream so the user sees a response body not an empty body  
            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing request logging middleware");
            throw;
        }
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        var sb = new StringBuilder();
        
        // Logging headers
        sb.AppendLine("Headers:");
        foreach (var header in request.Headers)
        {
            sb.AppendLine($"  {header.Key}: {header.Value}");
        }

        // Logging body if present
        // In a GET endpoint not request body is present
        if (request.Body.CanRead)
        {
            request.EnableBuffering();
            var bodyText = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Position = 0;  // Reseting the position for other middlewares

            if (!string.IsNullOrEmpty(bodyText))
            {
                sb.AppendLine("Body:");
                sb.AppendLine(bodyText);
            }
        }

        return sb.ToString();
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        return responseBody;
    }
}


public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}