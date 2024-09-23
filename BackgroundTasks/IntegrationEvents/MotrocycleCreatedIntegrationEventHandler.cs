using MediatR;
using Moto.Application.Motorcycles.CreatedMotorcycle;

namespace Moto.BackgroundTasks.IntegrationEvents;

internal class MotrocycleCreatedIntegrationEventHandler : INotificationHandler<MotorcycleCreatedIntegrationEvent>
{
    public Task Handle(MotorcycleCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
