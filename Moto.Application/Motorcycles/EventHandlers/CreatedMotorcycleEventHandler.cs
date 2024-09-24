using MediatR;
using Microsoft.Extensions.Logging;
using Moto.Application.Bus;
using Moto.Application.Motorcycles.IntegrationEvents;
using Moto.Domain.Events;

namespace Moto.Application.Motorcycles.EventHandlers;

/// <summary>
/// Handles the event when a motorcycle is created by publishing an integration event.
/// </summary>
/// <param name="_logger">An instance of <see cref="ILogger{T}"/> for logging information and errors.</param>
/// <param name="_eventPublisher">The event publisher used to publish integration events.</param>
public sealed class CreatedMotorcycleEventHandler(
    ILogger<CreatedMotorcycleEventHandler> _logger,
    IEventPublisher _eventPublisher) : INotificationHandler<MotorcycleCreatedEvent>
{
    public async Task Handle(MotorcycleCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Publishing motorcycle created integration event {@Notification}", notification);

        _eventPublisher.Publish(new MotorcycleCreatedIntegrationEvent(notification));
    }
}