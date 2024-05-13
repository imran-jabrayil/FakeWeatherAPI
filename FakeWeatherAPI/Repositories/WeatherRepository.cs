using FakeWeatherAPI.Models;
using FakeWeatherAPI.Repositories.Abstractions;
using FakeWeatherAPI.Repositories.Options;
using Microsoft.Extensions.Options;

namespace FakeWeatherAPI.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly int _timeDiff;
    private readonly Dictionary<string, WeatherForecast> _weatherForecasts = new();


    public WeatherRepository(IOptions<WeatherRepositoryOptions> options)
    {
        _timeDiff = options.Value.DelayInMinutes;
        Console.WriteLine(_timeDiff);
    }
    
    
    public WeatherForecast GetWeatherForecast(string cityName)
    {
        this._updateWeather(cityName);
        return _weatherForecasts[cityName];
    }


    private void _updateWeather(string cityName)
    {
        if (_weatherForecasts.TryGetValue(cityName, out WeatherForecast? weatherForecast) && 
            (DateTime.Now - weatherForecast.LastUpdated).Minutes < _timeDiff)
        {
            return;
        }
        
        
        Random random = new Random(DateTime.Now.Millisecond);
        _weatherForecasts[cityName] = new WeatherForecast(random.Next(10, 30), DateTime.Now);
    }
}