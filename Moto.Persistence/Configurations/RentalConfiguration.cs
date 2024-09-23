using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moto.Domain.Entities;
using Moto.Persistence.Extensions;

namespace Moto.Persistence.Configurations;

/// <summary>
/// Configures the entity properties and constraints for the Rental entity.
/// Implements the <see cref="IEntityTypeConfiguration{Rental}"/> interface, 
/// used by Entity Framework to configure the model via the Fluent API.
/// </summary>
internal sealed class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.TotalPayment).HasPrecision(7, 2);

        builder.HasOne(x => x.Courier)
            .WithMany()
            .HasForeignKey(x => x.CourierId);

        builder.HasOne(x => x.Motorcycle)
            .WithMany()
            .HasForeignKey(x => x.MotorcycleId);
    }
}
