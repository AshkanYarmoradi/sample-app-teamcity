using System.Net;
using Sample_TeamCity.Integration_Test.Factory;

namespace Sample_TeamCity.Integration_Test.Tests;

public class WeatherControllerTest: IClassFixture<TeamCityWebApplicationFactory>
{
    private static readonly string BaseUrl = "api/weather";
    private readonly HttpClient _httpClient;
    
    public WeatherControllerTest(TeamCityWebApplicationFactory teamCityWebApplicationFactory)
    {
        this._httpClient = teamCityWebApplicationFactory.CreateDefaultClient();
    }
    
    [Fact]
    public async Task Get_WeatherController_SuccessStatus()
    {
        var response  = await this._httpClient.GetAsync($"{BaseUrl}/");

        response.EnsureSuccessStatusCode();

        var temperatures = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();
        
        Assert.NotNull(temperatures);
        
#pragma warning disable CS8602
        Assert.Equal(3, temperatures.Count);
#pragma warning restore CS8602
    }
    
    [Theory]
    [InlineData("Amsterdam")]
    [InlineData("Utrecht")]
    public async Task GetByCityName_WeatherController_NotFoundException(string cityName)
    {
        var response  = await this._httpClient.GetAsync($"{BaseUrl}/city/{cityName}");
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var message = await response.Content.ReadAsStringAsync();
        
        Assert.Equal("City Not Found!", message);
    }
    
    [Theory]
    [InlineData("Kermanshah", 20)]
    [InlineData("KerManshah", 20)]
    [InlineData("Denhaag", 18)]
    public async Task GetByCityName_WeatherController_SuccessStatus(string cityName, int temperature)
    {
        var response  = await this._httpClient.GetAsync($"{BaseUrl}/city/{cityName}");

        response.EnsureSuccessStatusCode();
        
        Assert.Equal(temperature.ToString(), await response.Content.ReadAsStringAsync());
    }

    [Theory]
    [InlineData("Kermanshah", 20)]
    [InlineData("KerManshah", 20)]
    [InlineData("Denhaag", 18)]
    public async Task GetByCityNameRealTime_WeatherController_SuccessStatus(string cityName, int temperature)
    {
        var response  = await this._httpClient.GetAsync($"{BaseUrl}/city/{cityName}/realtime");

        response.EnsureSuccessStatusCode();
        
        Assert.Equal(temperature.ToString(), await response.Content.ReadAsStringAsync());
    }
}