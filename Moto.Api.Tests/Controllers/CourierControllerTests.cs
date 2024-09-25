using FluentAssertions;
using Moto.Application.UseCases.Couriers.Commands;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Moto.Api.Tests.Motorcycle;

public class CourierControllertTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    private JsonSerializerOptions _options => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public CourierControllertTests(CustomWebApplicationFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Should_Return_Success_When_Request_Correct()
    {
        CreateCourier courier = new("70319171000163", new DateOnly(1990, 10, 25), "14035198001", "A", string.Empty);

        var response = await _httpClient
            .PostAsJsonAsync("api/entregadores", courier, _options);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Should_Return_Error_When_Cnpj_Is_Empty()
    {
        CreateCourier courier = new(string.Empty, new DateOnly(1990, 10, 25), "14035198001", "A", string.Empty);

        var response = await _httpClient
            .PostAsJsonAsync("api/entregadores", courier, _options);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Return_Error_When_Cnpj_Is_invalid()
    {
        CreateCourier courier = new("70319171000162", new DateOnly(1990, 10, 25), "14035198001", "A", string.Empty);

        var response = await _httpClient
            .PostAsJsonAsync("api/entregadores", courier, _options);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Return_Error_When_Cnh_Is_Empty()
    {
        CreateCourier courier = new("70319171000163", new DateOnly(1990, 10, 25), string.Empty, "A", string.Empty);

        var response = await _httpClient
            .PostAsJsonAsync("api/entregadores", courier, _options);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Return_Error_When_Cnh_Is_Invalid()
    {
        CreateCourier courier = new("70319171000163", new DateOnly(1990, 10, 25), "14035198000", "A", string.Empty);

        var response = await _httpClient
            .PostAsJsonAsync("api/entregadores", courier, _options);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Return_Error_When_Driving_License_Type_Is_Empty()
    {
        CreateCourier courier = new("70319171000163", new DateOnly(1990, 10, 25), "14035198001", string.Empty, string.Empty);

        var response = await _httpClient
            .PostAsJsonAsync("api/entregadores", courier, _options);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
