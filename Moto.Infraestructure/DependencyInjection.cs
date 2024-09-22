using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moto.Application.Bus;
using Moto.Application.Storage;
using Moto.Infraestructure.Bus;
using Moto.Infraestructure.Storage;

namespace Moto.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IFileStorageService, LocalFileStorageService>();

        services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.SettingsKey));

        services.AddScoped<IEventPublisher, RabbitMQEventPublisher>();

        return services;
    }
}
