using Microsoft.AspNetCore.Mvc;
using Sample_TeamCity.Exceptions;
using Sample_TeamCity.Services;

namespace Sample_TeamCity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        this._weatherService = weatherService;
    }
    
    public IActionResult Get()
    {
        var result = this._weatherService.GetTemperatureCities();
        return Ok(result);
    }
    
    [Route("city/{cityName}")]
    public IActionResult GetByCityName(string cityName)
    {
        try
        {
            var result = this._weatherService.GetTemperatureByCity(cityName);

            return Ok(result);
        }
        catch (NotFoundCityException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [Route("city/{cityName}/realtime")]
    public async Task<IActionResult> GetByCityNameRealTime(string cityName)
    {
        try
        {
            var result = await this._weatherService.GetTemperatureByCityRealTimeAsync(cityName);

            return Ok(result);
        }
        catch (NotFoundCityException ex)
        {
            return NotFound(ex.Message);
        }
    }
}