using FakeWeatherAPI.Models;
using FakeWeatherAPI.Repositories.Abstractions;

namespace FakeWeatherAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ICollection<User> Users = new List<User>();
    
    
    public User? GetUserByUsername(string username)
    {
        return Users.FirstOrDefault(u => u.Username == username);
    }

    public User RegisterUser(string username)
    {
        User? user = Users.FirstOrDefault(u => u.Username == username);
        if (user is not null)
        {
            return user;
        }

        User newUser = new(username, new List<City>());
        Users.Add(newUser);
        return newUser;
    }

    public User? AddCity(string username, string cityName)
    {
        User? user = Users.FirstOrDefault(u => u.Username == username);
        
        if (user?.Cities.Any(c => c.Name == cityName) is false)
        {
            user.Cities.Add(new City(cityName));
        }

        return user;
    }

    public IEnumerable<City> GetCities(string username)
    {
        User? user = Users.FirstOrDefault(u => u.Username == username);
        return user?.Cities.AsEnumerable() ?? [];
    }

    public bool Exists(string username) => Users.Any(user => user.Username == username);
}