using MediatR;
using Moto.Application.Motorcycles.Queries;
using Moto.Application.Motorcycles.Responses;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.QueryHandlers;

public sealed class GetAllMotrocyclesHandler(
    IMotorcyleRepository _repository) : IRequestHandler<GetAllMotrocycles, Result<List<MotorcycleResponse>>>
{
    public async Task<Result<List<MotorcycleResponse>>> Handle(GetAllMotrocycles request, CancellationToken cancellationToken)
    {
        var motorcycles = await _repository.ListAllAsync(request.Placa, cancellationToken);

        var response = motorcycles.Select(x =>
            new MotorcycleResponse(
                x.Id,
                x.Year,
                x.Model,
                x.LicensePlate.Value)
            ).ToList();

        return Result.Success(response);
    }
}