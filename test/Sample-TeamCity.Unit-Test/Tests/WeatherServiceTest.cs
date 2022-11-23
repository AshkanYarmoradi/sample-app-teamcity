using Sample_TeamCity.Exceptions;
using Sample_TeamCity.Services;

namespace Sample_TeamCity.Unit_Test.Tests;

public class WeatherServiceTest: IClassFixture<WeatherService>
{
    private readonly WeatherService _weatherService;
    
    public WeatherServiceTest(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    
    [Fact]
    public void GetTemperatureCities_WeatherService_SuccessStatus()
    {
        var temperatures = _weatherService.GetTemperatureCities();
        
        Assert.NotNull(temperatures);
        
#pragma warning disable CS8602
        Assert.Equal(3, temperatures.Count);
#pragma warning restore CS8602
    }
    
    [Theory]
    [InlineData("Kermanshah", 20)]
    [InlineData("KerManshah", 20)]
    [InlineData("Denhaag", 18)]
    public void GetByCityName_WeatherService_SuccessStatus(string cityName, int temperature)
    {
        var result = _weatherService.GetTemperatureByCity(cityName);
        
        Assert.Equal(temperature, result);
    }

    [Theory]
    [InlineData("Kermanshah", 20)]
    [InlineData("KerManshah", 20)]
    [InlineData("Denhaag", 18)]
    public async Task GetByCityNameRealTime_WeatherService_SuccessStatus(string cityName, int temperature)
    {
        var result = await _weatherService.GetTemperatureByCityRealTimeAsync(cityName);
        
        Assert.Equal(temperature, result);
    }
    
    [Theory]
    [InlineData("Amsterdam")]
    [InlineData("Utrecht")]
    public async Task GetByCityNameRealTime_WeatherService_NotFoundException(string cityName)
    {
        await Assert.ThrowsAsync<NotFoundCityException>(async () =>
        {
            await _weatherService.GetTemperatureByCityRealTimeAsync(cityName);
        });
    }
}