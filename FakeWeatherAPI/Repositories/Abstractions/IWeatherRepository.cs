using FakeWeatherAPI.Models;

namespace FakeWeatherAPI.Repositories.Abstractions;

public interface IWeatherRepository
{
    public WeatherForecast GetWeatherForecast(string cityName);
}