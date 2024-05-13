namespace FakeWeatherAPI.Models;

public record User(string Username, ICollection<City> Cities);