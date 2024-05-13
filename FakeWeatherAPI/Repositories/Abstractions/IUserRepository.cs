using FakeWeatherAPI.Models;

namespace FakeWeatherAPI.Repositories.Abstractions;

public interface IUserRepository
{
    public User? GetUserByUsername(string username);
    public User RegisterUser(string username);
    public User? AddCity(string username, string cityName);
    public IEnumerable<City> GetCities(string username);
    public bool Exists(string username);
}