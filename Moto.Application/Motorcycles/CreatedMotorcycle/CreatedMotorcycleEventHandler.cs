using MediatR;
using Moto.Application.Bus;
using Moto.Domain.Events;

namespace Moto.Application.Motorcycles.CreatedMotorcycle;

public sealed class CreatedMotorcycleEventHandler(
    IEventPublisher _eventPublisher) : INotificationHandler<MotorcycleCreatedEvent>
{
    public async Task Handle(MotorcycleCreatedEvent notification, CancellationToken cancellationToken)
    {
        _eventPublisher.Publish(new MotorcycleCreatedIntegrationEvent(notification));
    }
}