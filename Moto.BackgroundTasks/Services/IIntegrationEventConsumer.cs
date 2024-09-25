using Moto.Application.Contracts.Event;

namespace Moto.BackgroundTasks.Services;

internal interface IIntegrationEventConsumer
{
    void Consume(IIntegrationEvent integrationEvent);
}
