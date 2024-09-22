using MediatR;

namespace Moto.Application.Rentals.CompleteRental;

public sealed record CompleteRentalCommand(int Id, DateTime dataDevolucao) : IRequest<Unit>;
