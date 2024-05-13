using FakeWeatherAPI.Models;
using FakeWeatherAPI.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FakeWeatherAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : Controller
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly IUserRepository _userRepository;


    public WeatherController(IWeatherRepository weatherRepository, IUserRepository userRepository)
    {
        _weatherRepository = weatherRepository;
        _userRepository = userRepository;
    }


    [HttpGet]
    public IActionResult Get([FromQuery] string city)
    {
        WeatherForecast forecast = _weatherRepository.GetWeatherForecast(city);
        return Ok(forecast);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetFavorites([FromQuery] string username)
    {
        if (!_userRepository.Exists(username))
        {
            return NotFound("User with this username is not found");
        }
        IEnumerable<City> cities = _userRepository.GetCities(username);
        IEnumerable<WeatherForecast> forecasts = cities.Select(
            c => _weatherRepository.GetWeatherForecast(c.Name));
        return Ok(forecasts);
    }
}