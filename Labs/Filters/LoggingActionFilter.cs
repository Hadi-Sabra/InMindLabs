using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Lab1.Filters;

public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;
    private DateTime startTime;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }

    // Before the action method runs
    public void OnActionExecuting(ActionExecutingContext context)
    {
        startTime = DateTime.UtcNow;
        
        _logger.LogInformation("Action Starting: {ActionName}", context.ActionDescriptor.DisplayName);
        
        _logger.LogInformation("Start time: {startTime}",startTime);

        
        if (context.ActionArguments.Any())
        {
            var parameters = JsonSerializer.Serialize(context.ActionArguments);
            _logger.LogInformation("Parameters: {Params}", parameters);
        }
    }

    // After the action method runs
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var endTime = DateTime.UtcNow;
        var elapsedTime = endTime - startTime;
        _logger.LogInformation("Action Finished: {ActionName}", context.ActionDescriptor.DisplayName);
        
        _logger.LogInformation("End time: {endTime}", endTime);
        
        _logger.LogInformation("Duration: {Duration}", elapsedTime);

        // Logging response details
        if (context.Result != null)
        {
            var resultType = context.Result.GetType().Name;
            _logger.LogInformation("Result Type: {ResultType}", resultType);

            if (context.Result is Microsoft.AspNetCore.Mvc.ObjectResult objectResult)
            {
                _logger.LogInformation("Status Code: {StatusCode}", objectResult.StatusCode);
                _logger.LogInformation("Response Body: {ResponseBody}", JsonSerializer.Serialize(objectResult.Value));
            }
        }
        
        if (context.Exception != null)
        {
            _logger.LogError(context.Exception, "Exception occurred in action: {ActionName}", context.ActionDescriptor.DisplayName);
        }
    }
}