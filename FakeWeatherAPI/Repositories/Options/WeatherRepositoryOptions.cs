namespace FakeWeatherAPI.Repositories.Options;

public record WeatherRepositoryOptions 
{
    public int DelayInMinutes { get; set; }
}