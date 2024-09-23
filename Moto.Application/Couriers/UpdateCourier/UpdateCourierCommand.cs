using MediatR;

namespace Moto.Application.Couriers.UpdateCourier;

public sealed record UpdateCourierCommand(int Id, string? ImagemCNH): IRequest<Unit>;
