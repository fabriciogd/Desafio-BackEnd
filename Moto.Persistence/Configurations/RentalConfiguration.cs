using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moto.Domain.Entities;
using Moto.Persistence.Extensions;

namespace Moto.Persistence.Configurations;

internal sealed class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.TotalPayment).HasPrecision(7, 5);

        builder.HasOne(x => x.Courier)
            .WithMany()
            .HasForeignKey(x => x.CourierId);

        builder.HasOne(x => x.Motorcycle)
            .WithMany()
            .HasForeignKey(x => x.MotorcycleId);
    }
}
