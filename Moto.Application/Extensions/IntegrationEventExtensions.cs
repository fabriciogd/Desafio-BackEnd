using Moto.Application.Base;
using Moto.Domain.Entities;
using Newtonsoft.Json;

namespace Moto.Application.Extensions;

/// <summary>
/// Provides extension methods for integration events.
/// </summary>
public static class IntegrationEventExtensions
{
    /// <summary>
    /// Converts an integration event to a JSON event data format.
    /// </summary>
    /// <param name="event">The integration event to be converted.</param>
    /// <returns>An <see cref="Event"/> containing the type name and serialized JSON data of the event.</returns>
    public static Event ToJsonEventData(this IIntegrationEvent @event)
    {
        return Event.Create(
            @event.GetGenericTypeName(),
            JsonConvert.SerializeObject(@event)
        );
    }
}
