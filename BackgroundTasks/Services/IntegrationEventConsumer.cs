using MediatR;
using Moto.Application.Base;

namespace Moto.BackgroundTasks.Services;

internal sealed class IntegrationEventConsumer : IIntegrationEventConsumer
{
    private readonly IMediator _mediator;

    public IntegrationEventConsumer(IMediator mediator) => _mediator = mediator;

    public void Consume(IIntegrationEvent integrationEvent) => _mediator.Publish(integrationEvent).GetAwaiter().GetResult();
}
