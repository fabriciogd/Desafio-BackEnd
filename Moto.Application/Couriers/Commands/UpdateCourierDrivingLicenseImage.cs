using MediatR;
using Moto.Domain.Primitives;

namespace Moto.Application.Couriers.Commands;

public sealed record UpdateCourierDrivingLicenseImage(int Id, string? ImagemCNH) : IRequest<Result>;
