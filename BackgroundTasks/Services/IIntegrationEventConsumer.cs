using Moto.Application.Base;

namespace Moto.BackgroundTasks.Services;

internal interface IIntegrationEventConsumer
{
    void Consume(IIntegrationEvent integrationEvent);
}
