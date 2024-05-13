using AutoFixture.Xunit2;
using FakeWeatherAPI.Models;
using FakeWeatherAPI.Repositories;
using FluentAssertions;

namespace FakeWeatherAPI.Tests.Repositories;

public class UserRepositoryTests
{
    [Theory, InlineAutoData]
    public void GetUserByUsername_UserDoesNotExist_ReturnedNull(
        UserRepository sut, string username)
    {
        // Act
        User? user = sut.GetUserByUsername(username);
        
        // Assert
        user.Should().BeNull();
    }


    [Theory, InlineAutoData]
    public void GetUserByUsername_UserExists_UserReturned(
        UserRepository sut, string username)
    {
        // Arrange
        sut.RegisterUser(username);
        
        // Act
        User? user = sut.GetUserByUsername(username);
        
        // Assert
        user.Should().NotBeNull();
        user!.Username.Should().Be(username);
        user.Cities.Should().BeEmpty();
    }
}