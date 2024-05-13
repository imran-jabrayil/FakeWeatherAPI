using FakeWeatherAPI.Models;
using FakeWeatherAPI.Models.Requests;
using FakeWeatherAPI.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FakeWeatherAPI.Controllers;


[ApiController]
[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    [HttpGet]
    public IActionResult Get([FromQuery] string username)
    {
        User? user = _userRepository.GetUserByUsername(username);
        return user is not null
            ? Ok(user)
            : NotFound(username);
    }
    
    
    [HttpPost]
    public IActionResult Create([FromBody] CreateUserRequest request)
    {
        User user = _userRepository.RegisterUser(request.Username);
        return Ok(user);
    }


    [HttpPost]
    public IActionResult AddCities([FromBody] AddCitiesRequest request)
    {
        (string username, string[] cityNames) = request;
        
        User? user = _userRepository.GetUserByUsername(username);
        if (user is null)
        {
            return NotFound(username);
        }
        
        cityNames.ToList()
            .ForEach(name => _userRepository.AddCity(username, name));
        return Ok(user);
    }
}