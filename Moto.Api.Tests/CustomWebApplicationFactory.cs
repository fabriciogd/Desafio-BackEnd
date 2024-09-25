﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moto.Persistence.Contexts;
using Npgsql;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using Testcontainers.PostgreSql;
using Microsoft.Extensions.Logging;
using Moto.Application.Bus;
using Moq;
using Moto.Application.Base;
using Microsoft.AspNetCore.TestHost;

namespace Moto.Api.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;
    public CustomWebApplicationFactory()
    {
        _dbContainer = new PostgreSqlBuilder().Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<MotoDbContext>));

            services.RemoveAll(typeof(IDbConnectionFactory));

            services.TryAddSingleton<IDbConnectionFactory>(_ =>
                new NpgsqlConnectionFactory(_dbContainer.GetConnectionString())
            );

            services.AddDbContext<MotoDbContext>(options =>
                options.UseNpgsql(_dbContainer.GetConnectionString()));

            services.Mock<IEventPublisher>(mock =>
            {
                mock.Setup(x => x.Publish(It.IsAny<IIntegrationEvent>()));
            });
        });

        base.ConfigureWebHost(builder);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using var scope = Services.CreateScope();
        var databaseContext = scope.ServiceProvider.GetService<MotoDbContext>();
        await databaseContext.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    private class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}