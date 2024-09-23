using MediatR;
using Moto.Application.Couriers.Queries;
using Moto.Application.Couriers.Responses;
using Moto.Domain.Primitives;
using Moto.Domain.Repositories;

namespace Moto.Application.Couriers.QueryHandlers;

public class GetAllCouriersHandler(
    ICourierRepository _repository) : IRequestHandler<GetAllCouriers, Result<List<CourierResponse>>>
{
    public async Task<Result<List<CourierResponse>>> Handle(GetAllCouriers request, CancellationToken cancellationToken)
    {
        var motorcycles = await _repository.GetAllAsync(cancellationToken);

        var response = motorcycles.Select(x =>
            new CourierResponse(
                x.Id,
                x.Cnpj,
                x.BirthDate,
                x.DrivingLicense.Value,
                x.DrivingLicenseType,
                x.DrivingLicenseImagePath)
            ).ToList();

        return Result.Success(response);
    }
}
