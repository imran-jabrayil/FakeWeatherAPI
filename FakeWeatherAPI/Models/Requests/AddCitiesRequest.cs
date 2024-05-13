namespace FakeWeatherAPI.Models.Requests;

public record AddCitiesRequest(
    string Username, 
    string[] CityNames);