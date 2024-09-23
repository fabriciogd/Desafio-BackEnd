using Microsoft.EntityFrameworkCore;
using Moto.Persistence.Configurations;

namespace Moto.Persistence.Contexts;

/// <summary>
/// The MotoDbContext class represents the Entity Framework database context for the application.
/// It inherits from <see cref="DbContext"/> and is configured with the provided options for database connection.
/// This class is responsible for managing the interaction with the database, including the definition of the models (entities) and their configurations.
/// </summary>
/// <param name="dbOptions">The options to configure the DbContext, such as the connection string and provider.</param>
public class MotoDbContext:DbContext
{ 
    public MotoDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new MotorcycleConfiguration());
        modelBuilder.ApplyConfiguration(new CourierConfiguration());
        modelBuilder.ApplyConfiguration(new PlanConfiguration());
        modelBuilder.ApplyConfiguration(new RentalConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
    }
}
