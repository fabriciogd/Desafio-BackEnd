using Moto.Application.Base;

namespace Moto.Application.Bus;

public interface IEventPublisher
{
    void Publish(IIntegrationEvent @event);
}
