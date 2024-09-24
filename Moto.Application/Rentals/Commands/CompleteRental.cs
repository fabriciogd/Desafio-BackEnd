using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Rentals.Commands;

public sealed record CompleteRental(int Id, DateTime dataDevolucao) : IRequest<Result>;
