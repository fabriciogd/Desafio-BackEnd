using Microsoft.Extensions.Options;
using Moto.Application.Base;
using Moto.Application.Bus;
using Newtonsoft.Json;
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

        _channel.QueueDeclare(_messageBrokerSettings.QueueName, false, false, false);
    }

    public void Publish(IIntegrationEvent @event)
    {
        string payload = JsonConvert.SerializeObject(@event, typeof(IIntegrationEvent), new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });

        byte[] body = Encoding.UTF8.GetBytes(payload);

        _channel.BasicPublish(string.Empty, _messageBrokerSettings.QueueName, body: body);
    }

    public void Dispose()
    {
        _connection?.Dispose();

        _channel?.Dispose();
    }
}
