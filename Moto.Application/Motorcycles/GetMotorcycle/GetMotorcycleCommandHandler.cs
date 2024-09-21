using MediatR;
using Moto.Application.Motorcycles.Response;
using Moto.Domain.Errors;
using Moto.Domain.Exceptions;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.GetMotorcycle;

public sealed class GetMotorcycleCommandHandler(
    IMotorcyleRepository _repository) : IRequestHandler<GetMotorcycleCommand, MotorcycleResponse>
{
    public async Task<MotorcycleResponse> Handle(GetMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _repository.FindByIdentificadorAsync(request.Id, cancellationToken);

        if (motorcycle is null)
            throw new NotFoundException(DomainErrors.Motorcycle.NotFound);

        var response = new MotorcycleResponse(
            motorcycle.Identificador,
            motorcycle.Ano,
            motorcycle.Modelo,
            motorcycle.Placa
        );

        return response;
    }
}
