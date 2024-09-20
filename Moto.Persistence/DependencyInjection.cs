using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moto.Application.Interfaces;
using Moto.Domain.Repositories;
using Moto.Persistence.Contexts;
using Moto.Persistence.Repositories;

namespace Moto.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(ConnectionString.Default)!;

        services.AddDbContext<MotoDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMotorcyleRepository, MotorcycleRepository>();

        return services;
    }
}