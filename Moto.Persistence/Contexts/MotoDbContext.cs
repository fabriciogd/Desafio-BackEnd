using Microsoft.EntityFrameworkCore;
using Moto.Domain.Entities;
using Moto.Persistence.Configurations;

namespace Moto.Persistence.Contexts;

public class MotoDbContext(DbContextOptions<MotoDbContext> dbOptions)
    : DbContext(dbOptions)
{
    public DbSet<Motorcycle> Motorcycles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new MotorcycleConfiguration());
        modelBuilder.ApplyConfiguration(new CourierConfiguration());
    }
}
