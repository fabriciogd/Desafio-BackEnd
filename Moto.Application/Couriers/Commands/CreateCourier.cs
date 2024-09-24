using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Couriers.Commands;

/// <summary>
/// Represents a command to create a new courier.
/// This record implements <see cref="IRequest{Result}"/> for handling requests in a CQRS pattern.
/// </summary>
/// <param name="Cnpj">The CNPJ of the courier.</param>
/// <param name="DataNascimento">The date of birth of the courier.</param>
/// <param name="NumeroCnh">The driver's license number of the courier.</param>
/// <param name="TipoCnh">The type of driver's license held by the courier.</param>
/// <param name="ImagemCnh">The image of the courier's driver's license.</param>
public sealed record CreateCourier(
    string? Cnpj,
    DateTime DataNascimento,
    string? NumeroCnh,
    string? TipoCnh,
    string? ImagemCnh) : IRequest<Result>;