using MediatR;
using Moto.Application.Interfaces;
using Moto.Domain.Errors;
using Moto.Domain.Exceptions;
using Moto.Domain.Repositories;

namespace Moto.Application.Motorcycles.DeleteMotorcycle;

public sealed class DeleteMotorcycleCommandHandler(
    IMotorcyleRepository _repository,
    IUnitOfWork _unitOfWork) : IRequestHandler<DeleteMotorcycleCommand, Unit>
{
    public async Task<Unit> Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _repository.FindByIdentificatorAsync(request.Id, cancellationToken);

        if (motorcycle is null)
            throw new NotFoundException(DomainErrors.Motorcycle.NotFound);

        _repository.Remove(motorcycle);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
