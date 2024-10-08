﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moto.Application.Contracts.Context;
using Moto.Domain.Repositories;
using Moto.Persistence.Contexts;
using Moto.Persistence.Repositories;

namespace Moto.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(ConnectionString.Default)!;

        services.AddDbContext<MotoDbContext>(options => options.UseNpgsql(connectionString, options =>
        {
            options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                null
            );
        }));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMotorcyleRepository, MotorcycleRepository>();
        services.AddScoped<ICourierRepository, CourierRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IEventRepository, EventRepository>();

        return services;
    }
}