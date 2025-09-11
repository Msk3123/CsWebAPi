using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPItesting.Controller;


[Route("[controller]")]
[ApiController]
public class WeatherForecastController: ControllerBase
{
    private ILoggerManager _logger;

    public WeatherForecastController(ILoggerManager loggerManager)
    {
        _logger = loggerManager;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
        _logger.LogInfo("Here is info message from our values controller.");
        _logger.LogDebug("Debugging information logged.");
        _logger.LogWarn("This is a warning message.");
        _logger.LogError("An error occurred while processing the request.");
        
        
        return new string[] { "value1", "value2" };
    }
}