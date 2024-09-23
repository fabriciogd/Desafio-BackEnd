using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moto.BackgroundTasks.Services;
using Moto.BackgroundTasks.Tasks;
using System.Reflection;

namespace Moto.BackgroundTasks;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddHostedService<IntegrationEventConsumerBackgroundService>();
        services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();

        return services;
    }
}
