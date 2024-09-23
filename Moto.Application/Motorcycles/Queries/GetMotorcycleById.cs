using MediatR;
using Moto.Application.Motorcycles.Responses;
using Moto.Domain.Primitives;

namespace Moto.Application.Motorcycles.Queries;

public sealed record GetMotorcycleById(int Id) : IRequest<Result<MotorcycleResponse>>;
