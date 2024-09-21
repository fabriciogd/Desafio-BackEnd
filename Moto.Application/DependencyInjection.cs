using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moto.Application.Behaviors;

namespace Moto.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(AssemblyReference.Assembly)
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });


        return services;
    }
}
