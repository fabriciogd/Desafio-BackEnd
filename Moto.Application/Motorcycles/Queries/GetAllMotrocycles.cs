using MediatR;
using Moto.Application.Motorcycles.Responses;
using Moto.Domain.Primitives;

namespace Moto.Application.Motorcycles.Queries;

public sealed record GetAllMotrocycles(string? Placa) : IRequest<Result<List<MotorcycleResponse>>>;