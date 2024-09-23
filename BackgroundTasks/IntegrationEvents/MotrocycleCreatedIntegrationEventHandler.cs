using MediatR;
using Moto.Application.Interfaces;
using Moto.Application.Motorcycles.CreatedMotorcycle;
using Moto.Domain.Repositories;
using Moto.Application.Extensions;

namespace Moto.BackgroundTasks.IntegrationEvents;

internal class MotrocycleCreatedIntegrationEventHandler(
    IEventRepository _repository,
    IUnitOfWork _unitOfWork) : INotificationHandler<MotorcycleCreatedIntegrationEvent>
{
    public async Task Handle(MotorcycleCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Ano is not 2024)
            return;

        await _repository.AddAsync(notification.ToJsonEventData(), cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
