using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moto.Domain.Entities;
using Moto.Persistence.Extensions;

namespace Moto.Persistence.Configurations;

/// <summary>
/// Configures the entity properties and constraints for the Motorcycle entity.
/// Implements the <see cref="IEntityTypeConfiguration{Motorcycle}"/> interface, 
/// used by Entity Framework to configure the model via the Fluent API.
/// </summary>
internal sealed class MotorcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.Identificador)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Placa)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(x => x.Modelo)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Ano)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Placa).IsUnique();
    }
}
