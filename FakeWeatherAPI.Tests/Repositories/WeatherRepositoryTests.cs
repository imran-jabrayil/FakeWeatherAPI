using AutoFixture.Xunit2;
using FakeWeatherAPI.Models;
using FakeWeatherAPI.Repositories;
using FakeWeatherAPI.Repositories.Options;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace FakeWeatherAPI.Tests.Repositories;

public class WeatherRepositoryTests
{
    [Theory, InlineAutoData]
    public void GetWeatherForecast_WeatherForecastReturned(
        string cityName)
    {
        // Arrange
        var sut = this._getSut(5);
        
        // Act
        WeatherForecast forecast = sut.GetWeatherForecast(cityName);
        
        // Assert
        forecast.Should().NotBeNull();
        forecast.TemperatureC.Should().BePositive()
            .And.BeGreaterThanOrEqualTo(10)
            .And.BeLessThanOrEqualTo(30);
        forecast.LastUpdated.Should().BeOnOrBefore(DateTime.Now);
    }
    
    [Theory, InlineAutoData]
    public void GetWeatherForecast_CalledTwice_SameWeatherForecastsReturned(
        string cityName)
    {
        var sut = this._getSut(int.MaxValue);
        
        // Act
        WeatherForecast forecast1 = sut.GetWeatherForecast(cityName);
        WeatherForecast forecast2 = sut.GetWeatherForecast(cityName);
        
        // Assert
        forecast1.Should().NotBeNull();
        forecast1.TemperatureC.Should().BePositive()
            .And.BeGreaterThanOrEqualTo(10)
            .And.BeLessThanOrEqualTo(30);
        forecast1.LastUpdated.Should().BeOnOrBefore(DateTime.Now);
        forecast1.Should().BeEquivalentTo(forecast2);
    }


    private WeatherRepository _getSut(int delay)
    {
        Mock<IOptions<WeatherRepositoryOptions>> optionsMock = new();
        WeatherRepositoryOptions options = new() { DelayInMinutes = delay }; 
        optionsMock.Setup(om => om.Value)
            .Returns(options);
        WeatherRepository sut = new(optionsMock.Object);
        return sut;
    }
}