using MediatR;
using Moto.Application.Couriers.Responses;
using Moto.Domain.Primitives;

namespace Moto.Application.Couriers.Queries;

public sealed record GetAllCouriers() : IRequest<Result<List<CourierResponse>>>;
