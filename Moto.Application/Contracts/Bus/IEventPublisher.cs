using Moto.Application.Contracts.Event;

namespace Moto.Application.Contracts.Bus;

/// <summary>
/// Defines the contract for an event publisher responsible for publishing integration events.
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// Publishes an integration event.
    /// </summary>
    /// <param name="event">The integration event to be published.</param>
    void Publish(IIntegrationEvent @event);
}