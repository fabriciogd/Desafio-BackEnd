using Microsoft.Extensions.DependencyInjection;
using Moto.Application.Storage;
using Moto.Infraestructure.Storage;

namespace Moto.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFileStorageService, LocalFileStorageService>();

        return services;
    }
}
