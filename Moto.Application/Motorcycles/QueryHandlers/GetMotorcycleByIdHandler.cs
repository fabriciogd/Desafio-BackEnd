using MediatR;
using Moto.Application.Motorcycles.Queries;
using Moto.Application.Motorcycles.Responses;
using Moto.Domain.Errors;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.QueryHandlers;

public sealed class GetMotorcycleByIdHandler(
    IMotorcyleRepository _repository) : IRequestHandler<GetMotorcycleById, Result<MotorcycleResponse>>
{
    public async Task<Result<MotorcycleResponse>> Handle(GetMotorcycleById request, CancellationToken cancellationToken)
    {
        var motorcycle = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (motorcycle is null)
            return Result<MotorcycleResponse>.NotFound(DomainErrors.Motorcycle.NotFound);

        var response = new MotorcycleResponse(
            motorcycle.Id,
            motorcycle.Year,
            motorcycle.Model,
            motorcycle.LicensePlate
        );

        return Result.Success(response);
    }
}
