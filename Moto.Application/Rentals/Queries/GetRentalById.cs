using MediatR;
using Moto.Application.Rents.Responses;
using Moto.Domain.Primitives;

namespace Moto.Application.Rentals.Queries;

public sealed record GetRentalById(int Id) : IRequest<Result<RentalResponse>>;