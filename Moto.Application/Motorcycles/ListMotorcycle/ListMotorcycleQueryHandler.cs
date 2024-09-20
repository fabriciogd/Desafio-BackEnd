using MediatR;
using Moto.Domain.Primitives.Result;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.ListMotorcycle
{
    public class ListMotorcycleQueryHandler(
        IMotorcyleRepository _repository) : IRequestHandler<ListMotorcycleQuery, Result<IEnumerable<ListMotorcycleResponse>>>
    {
        public async Task<Result<IEnumerable<ListMotorcycleResponse>>> Handle(ListMotorcycleQuery request, CancellationToken cancellationToken)
        {
            var motorcycles = await _repository.ListByPlateAsync(request.Placa, cancellationToken);

            var list = motorcycles.Select(x => 
                new ListMotorcycleResponse(
                    x.Id, 
                    x.Identificador, 
                    x.Ano, 
                    x.Modelo, 
                    x.Placa)
                );

            return Result<IEnumerable<ListMotorcycleResponse>>.Success(list);
        }
    }
}
