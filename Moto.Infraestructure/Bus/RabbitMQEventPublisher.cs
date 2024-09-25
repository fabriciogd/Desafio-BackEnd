using Microsoft.Extensions.Options;
using Moto.Application.Contracts.Bus;
using Moto.Application.Contracts.Event;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Moto.Infraestructure.Bus;

/// <summary>
/// Implements an event publisher using RabbitMQ for publishing integration events.
/// </summary>
internal sealed class RabbitMQEventPublisher : IEventPublisher, IDisposable
{
    private readonly MessageBrokerSettings _messageBrokerSettings;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMQEventPublisher"/> class.
    /// </summary>
    /// <param name="messageBrokerSettings">The configuration settings for the message broker.</param>
    public RabbitMQEventPublisher(IOptions<MessageBrokerSettings> messageBrokerSettings)
    {
        _messageBrokerSettings = messageBrokerSettings.Value;

        IConnectionFactory connectionFactory = new ConnectionFactory
        {
            HostName = _messageBrokerSettings.HostName,
            Port = _messageBrokerSettings.Port,
            UserName = _messageBrokerSettings.UserName,
            Password = _messageBrokerSettings.Password
        };

        _connection = connectionFactory.CreateConnection();

        _channel = _connection.CreateModel();

        _channel.QueueDeclare(_messageBrokerSettings.QueueName, false, false, false);
    }

    /// <summary>
    /// Publishes an integration event to the RabbitMQ queue.
    /// </summary>
    /// <param name="event">The integration event to be published.</param>
    public void Publish(IIntegrationEvent @event)
    {
        string payload = JsonConvert.SerializeObject(@event, typeof(IIntegrationEvent), new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });

        byte[] body = Encoding.UTF8.GetBytes(payload);

        _channel.BasicPublish(string.Empty, _messageBrokerSettings.QueueName, body: body);
    }

    /// <summary>
    /// Disposes of the resources used by the <see cref="RabbitMQEventPublisher"/> instance.
    /// </summary>
    public void Dispose()
    {
        _connection?.Dispose();

        _channel?.Dispose();
    }
}
