using FluentAssertions;
using Moto.Application.UseCases.Motorcycles.Commands;
using System.Net;
using System.Net.Http.Json;

namespace Moto.Api.Tests.Controllers;

[Collection("CreateMotorcycle")]
public class CreateMotorcycleCommand : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public CreateMotorcycleCommand(CustomWebApplicationFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Should_Return_Success_When_All_Request_Correct()
    {
        CreateMotorcycle motorcycle =
            new(2022, "Titan", "ABC-1234");

        var response = await _httpClient
            .PostAsJsonAsync("api/motos", motorcycle);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Should_Return_Error_When_Year_After_2025()
    {
        CreateMotorcycle motorcycle =
            new(2026, "Titan", "ABC-1222");

        var response = await _httpClient
            .PostAsJsonAsync("api/motos", motorcycle);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Return_Error_With_Same_Plate()
    {
        CreateMotorcycle motorcycle =
            new(2024, "Titan", "ABC-2222");

        var response = await _httpClient
            .PostAsJsonAsync("api/motos", motorcycle);

        motorcycle =
            new(2024, "Titan", "ABC-2222");

        response = await _httpClient
            .PostAsJsonAsync("api/motos", motorcycle);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}
