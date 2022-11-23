using Sample_TeamCity.Exceptions;

namespace Sample_TeamCity.Services;

public class WeatherService: IWeatherService
{
    private static readonly IReadOnlyDictionary<string, int> TemperatureCities = new Dictionary<string, int>()
    {
        { "kermanshah", 20 },
        { "denhaag", 18 },
        { "rotterdam", 19 }
    };
    
    public int GetTemperatureByCity(string cityName)
    {
        cityName = NormalizeCityName(cityName);
        
        if (TemperatureCities.TryGetValue(cityName, out var temperature))
        {
            return temperature;
        }

        throw new NotFoundCityException();
    }

    public async Task<int> GetTemperatureByCityRealTimeAsync(string cityName)
    {
        // Some expensive calculation
        await Task.Delay(5000);

        cityName = NormalizeCityName(cityName);

        return GetTemperatureByCity(cityName);
    }
    
    public Dictionary<string, int> GetTemperatureCities()
    {
        return TemperatureCities.ToDictionary(pair=> pair.Key, pair=> pair.Value);
    }

    private static string NormalizeCityName(string cityName)
    {
        return cityName
            .ToLower()
            .Trim();
    }
}