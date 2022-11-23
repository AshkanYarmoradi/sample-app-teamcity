namespace Sample_TeamCity.Services;

public interface IWeatherService
{
    int GetTemperatureByCity(string cityName);

    Task<int> GetTemperatureByCityRealTimeAsync(string cityName);

    Dictionary<string, int> GetTemperatureCities();
}