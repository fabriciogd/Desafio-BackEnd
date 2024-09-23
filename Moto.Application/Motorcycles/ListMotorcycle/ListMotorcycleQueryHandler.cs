using MediatR;
using Moto.Application.Motorcycles.Response;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.ListMotorcycle;

public sealed class ListMotorcycleQueryHandler(
    IMotorcyleRepository _repository) : IRequestHandler<ListMotorcycleQuery, IEnumerable<MotorcycleResponse>>
{
    public async Task<IEnumerable<MotorcycleResponse>> Handle(ListMotorcycleQuery request, CancellationToken cancellationToken)
    {
        var motorcycles = await _repository.ListAllAsync(request.Placa, cancellationToken);

        var response = motorcycles.Select(x => 
            new MotorcycleResponse(
                x.Id, 
                x.Year, 
                x.Model, 
                x.LicensePlate)
            );

        return response;
    }
}