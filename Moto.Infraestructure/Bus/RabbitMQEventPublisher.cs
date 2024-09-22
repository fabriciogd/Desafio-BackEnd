using Microsoft.Extensions.Options;
using Moto.Application.Bus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Moto.Infraestructure.Bus;

internal sealed class RabbitMQEventPublisher : IEventPublisher, IDisposable
{
    private readonly MessageBrokerSettings _messageBrokerSettings;
    private readonly IConnection _connection;
    private readonly IModel _channel;

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

        _channel.ExchangeDeclare(exchange: _messageBrokerSettings.ExchangeName, type: ExchangeType.Direct);
    }

    public void Publish<T>(string queueName, T @event)
    {
        _channel.QueueDeclare(queueName, true, false, false);

        string payload = JsonSerializer.Serialize(@event, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        byte[] body = Encoding.UTF8.GetBytes(payload);

        _channel.BasicPublish(_messageBrokerSettings.ExchangeName, routingKey: queueName, body: body);
    }

    public void Dispose()
    {
        _connection?.Dispose();

        _channel?.Dispose();
    }
}
