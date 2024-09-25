using Moto.Application.UseCases.Motorcycles.Commands;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using FluentAssertions;
using Moto.Application.UseCases.Couriers.Commands;
using Moto.Domain.Entities;
using Moto.Application.UseCases.Rentals.Commands;
using Moto.Api.Tests.Extensions;
using Moto.Application.UseCases.Motorcycles.Responses;
using Moto.Application.UseCases.Couriers.Responses;

namespace Moto.Api.Tests.Controllers
{
    public class RentalControllerTest: IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        private JsonSerializerOptions _options => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        public RentalControllerTest(CustomWebApplicationFactory appFactory)
        {
            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task Should_Return_Success_When_Request_Correct()
        {
            CreateMotorcycle motorcycle =
                new(2022, "Titan", "ABC-1234");

            var response = await _httpClient
                .PostAsJsonAsync("api/motos", motorcycle);

            var motorcycleEntity = await response.ParseTo<MotorcycleResponse>(_options);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            CreateCourier courier = new("70319171000163", new DateOnly(1990, 10, 25), "14035198001", "A", string.Empty);

            response = await _httpClient
                .PostAsJsonAsync("api/entregadores", courier, _options);

            var courierEntity = await response.ParseTo<CourierResponse>(_options);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            CreateRental rental = new(motorcycleEntity.Id, courierEntity.Id, 7);

            response = await _httpClient
                .PostAsJsonAsync("api/locacao", rental, _options);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
