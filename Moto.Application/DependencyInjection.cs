using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Moto.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(AssemblyReference.Assembly)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        return services;
    }
}
