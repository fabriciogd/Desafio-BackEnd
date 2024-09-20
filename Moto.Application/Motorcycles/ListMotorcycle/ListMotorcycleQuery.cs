using MediatR;
using Moto.Domain.Primitives.Result;

namespace Moto.Application.Motorcycles.ListMotorcycle;

public class ListMotorcycleQuery: IRequest<Result<IEnumerable<ListMotorcycleResponse>>>
{
    public string? Placa { get; set; }
}