using MediatR;
using Moto.Domain.Repositories;
using Moto.Application.Extensions;
using Moto.Application.Contracts.Context;
using Moto.Application.UseCases.Motorcycles.IntegrationEvents;

namespace Moto.BackgroundTasks.IntegrationEvents;

internal class MotrocycleCreatedIntegrationEventHandler(
    IEventRepository _repository,
    IUnitOfWork _unitOfWork) : INotificationHandler<MotorcycleCreatedIntegrationEvent>
{
    public async Task Handle(MotorcycleCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Year is not 2024)
            return;

        await _repository.AddAsync(notification.ToJsonEventData(), cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
