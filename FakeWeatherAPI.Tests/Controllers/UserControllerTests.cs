using AutoFixture.Xunit2;
using FakeWeatherAPI.Controllers;
using FakeWeatherAPI.Models;
using FakeWeatherAPI.Models.Requests;
using FakeWeatherAPI.Repositories.Abstractions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FakeWeatherAPI.Tests.Controllers;

public class UserControllerTests
{
    [Theory, InlineAutoData]
    public void UserController_OnGet_UserDataReturned(
        [Frozen] Mock<IUserRepository> userRepository,
        User expectedUser,
        string username)
    {
        // Arrange
        userRepository.Setup(ur => ur.GetUserByUsername(username))
            .Returns(expectedUser)
            .Verifiable();
        UserController sut = new (userRepository.Object);
        
        // Act
        IActionResult response = sut.Get(username);
        
        // Assert
        OkObjectResult? result = response as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().Be(expectedUser);
        userRepository.Verify();
    }

    [Theory, InlineAutoData]
    public void UserController_OnCreate_CreatedUserReturned(
        [Frozen] Mock<IUserRepository> userRepository,
        CreateUserRequest request,
        User expectedUser)
    {
        // Arrange
        userRepository.Setup(ur => ur.RegisterUser(request.Username))
            .Returns(expectedUser)
            .Verifiable();
        UserController sut = new (userRepository.Object);
        
        // Act
        IActionResult response = sut.Create(request);
        
        // Assert
        OkObjectResult? result = response as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().Be(expectedUser);
        userRepository.Verify();
    }
}