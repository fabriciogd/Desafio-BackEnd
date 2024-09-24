using MediatR;

namespace Moto.Application.Base;

/// <summary>
/// Represents an integration event in the system.
/// This interface extends <see cref="INotification"/> and serves as a marker for integration events.
/// </summary>
public interface IIntegrationEvent: INotification
{
}
