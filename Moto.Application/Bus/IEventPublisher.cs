namespace Moto.Application.Bus;

public interface IEventPublisher
{
    void Publish<T>(string queueName, T @event);
}
