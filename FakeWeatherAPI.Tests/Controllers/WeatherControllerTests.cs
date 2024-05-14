using AutoFixture.Xunit2;
using FakeWeatherAPI.Controllers;
using FakeWeatherAPI.Models;
using FakeWeatherAPI.Repositories.Abstractions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FakeWeatherAPI.Tests.Controllers;

public class WeatherControllerTests
{
    [Theory, InlineAutoData]
    public void WeatherController_OnGet_WeatherForecastReturned(
        [Frozen] Mock<IWeatherRepository> weatherRepository,
        [Frozen] Mock<IUserRepository> userRepository,
        WeatherForecast expectedForecast,
        string cityName)
    {
        // Arrange
        weatherRepository.Setup(wr => wr.GetWeatherForecast(cityName))
            .Returns(expectedForecast)
            .Verifiable();
        WeatherController sut = new (weatherRepository.Object, userRepository.Object);
        
        // Act
        IActionResult response = sut.Get(cityName);
        
        // Assert
        OkObjectResult? result = response as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().Be(expectedForecast);
        weatherRepository.Verify();
    }

    [Theory, InlineAutoData]
    public void WeatherController_OnGetFavorites_UserExists_WeatherForecastsReturned(
        [Frozen] Mock<IWeatherRepository> weatherRepository,
        [Frozen] Mock<IUserRepository> userRepository,
        WeatherForecast expectedForecast1,
        City city1,
        WeatherForecast expectedForecast2,
        City city2,
        string username)
    {
        // Arrange
        List<City> cities = new List<City>{ city1, city2 };
        userRepository.Setup(ur => ur.GetCities(username))
            .Returns(cities)
            .Verifiable();
        userRepository.Setup(ur => ur.Exists(username))
            .Returns(true)
            .Verifiable();
        
        weatherRepository.Setup(wr => wr.GetWeatherForecast(city1.Name))
            .Returns(expectedForecast1)
            .Verifiable();
        weatherRepository.Setup(wr => wr.GetWeatherForecast(city2.Name))
            .Returns(expectedForecast2)
            .Verifiable();
        WeatherController sut = new (weatherRepository.Object, userRepository.Object);
        
        // Act
        IActionResult response = sut.GetFavorites(username);
        
        // Assert
        OkObjectResult? result = response as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.As<IEnumerable<WeatherForecast>>().Should().BeEquivalentTo(
            new List<WeatherForecast>{ expectedForecast1, expectedForecast2 });
        userRepository.Verify();
        weatherRepository.Verify();
    }
}