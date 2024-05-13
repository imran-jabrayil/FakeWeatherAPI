namespace FakeWeatherAPI.Models;

public record WeatherForecast(
    int TemperatureC,
    DateTime LastUpdated)
{
    public int TemperatureF => TemperatureC * 9 / 5 + 32;
}