using MediatR;
using Moto.Domain.Primitives;
using System.Text.Json.Serialization;

namespace Moto.Application.Couriers.Commands;

/// <summary>
/// Represents a command to update the driver's license image for an existing courier.
/// This record implements <see cref="IRequest{Result}"/> for handling requests in a CQRS pattern.
/// </summary>
/// <param name="Id">The unique identifier of the courier whose driver's license image is being updated.</param>
/// <param name="ImagemCNH">The new image of the courier's driver's license.</param>
public sealed record UpdateCourierDrivingLicenseImage([property: JsonIgnore] int Id, string? ImagemCNH) : IRequest<Result>;
