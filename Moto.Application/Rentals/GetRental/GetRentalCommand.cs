using MediatR;
using Moto.Application.Rents.Response;

namespace Moto.Application.Rents.GetRental;

public sealed record GetRentalCommand(int Id): IRequest<RentalResponse>;