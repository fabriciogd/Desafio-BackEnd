using MediatR;
using Moto.Application.Bus;
using Moto.Domain.Events;

namespace Moto.Application.Motorcycles.CreatedMotorcycle;

public sealed class CreateMotorcycleEventHandler(
    IEventPublisher _eventPublisher) : INotificationHandler<MotorcycleCreatedEvent>
{
    public async Task Handle(MotorcycleCreatedEvent notification, CancellationToken cancellationToken)
    {
        _eventPublisher.Publish("motorcycle.created", notification);
    }
}