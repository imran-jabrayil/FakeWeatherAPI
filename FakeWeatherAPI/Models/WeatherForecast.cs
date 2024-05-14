namespace FakeWeatherAPI.Models;

public record WeatherForecast(
    string City,
    DateTime LastUpdated,
    int TemperatureC)
{
    public int TemperatureF => TemperatureC * 9 / 5 + 32;
}