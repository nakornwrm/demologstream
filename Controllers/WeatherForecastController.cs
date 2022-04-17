using Microsoft.AspNetCore.Mvc;

namespace DemoLogStream.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get(string? input1)
    {
        try
        {
            if(string.IsNullOrEmpty(input1)) throw new Exception("No input1 parameter, it will return blank");

            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
                    .ToArray();

            _logger.LogDebug($"Request with input1={input1}");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error occur");
            return new List<WeatherForecast>();
        }
    }
}
