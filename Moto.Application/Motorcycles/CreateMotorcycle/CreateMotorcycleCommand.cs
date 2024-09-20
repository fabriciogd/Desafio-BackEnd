using MediatR;
using Moto.Domain.Primitives.Result;

namespace Moto.Application.Motorcycles.CreateMotorcycle;

public sealed class CreateMotorcycleCommand : IRequest<Result<CreateMotorcycleResponse>>
{
    public string? Identificador { get; set; }

    public short Ano { get; set; }

    public string? Modelo { get; set; }

    public string? Placa { get; set; }
}
