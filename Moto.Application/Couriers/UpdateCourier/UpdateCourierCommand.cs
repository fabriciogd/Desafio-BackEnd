using MediatR;

namespace Moto.Application.Couriers.UpdateCourier;

public sealed record UpdateCourierCommand(string? Id, string? ImagemCNH): IRequest<Unit>;
