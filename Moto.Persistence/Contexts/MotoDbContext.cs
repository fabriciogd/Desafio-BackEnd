using Microsoft.EntityFrameworkCore;
using Moto.Domain.Entities;
using Moto.Persistence.Configurations;

namespace Moto.Persistence.Contexts;

public class MotoDbContext(DbContextOptions<MotoDbContext> dbOptions)
    : DbContext(dbOptions)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new MotorcycleConfiguration());
        modelBuilder.ApplyConfiguration(new CourierConfiguration());
        modelBuilder.ApplyConfiguration(new PlanConfiguration());
        modelBuilder.ApplyConfiguration(new RentalConfiguration());
    }
}
