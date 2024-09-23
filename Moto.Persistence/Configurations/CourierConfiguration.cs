using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moto.Domain.Entities;
using Moto.Persistence.Extensions;

namespace Moto.Persistence.Configurations;

/// <summary>
/// Configures the entity properties and constraints for the Courier entity.
/// Implements the <see cref="IEntityTypeConfiguration{Courier}"/> interface, 
/// used by Entity Framework to configure the model via the Fluent API.
/// </summary>
internal sealed class CourierConfiguration : IEntityTypeConfiguration<Courier>
{
    public void Configure(EntityTypeBuilder<Courier> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.Cnpj)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.DrivingLicense)
            .IsRequired()
            .HasMaxLength(9);

        builder.Property(x => x.DrivingLicenseType)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(x => x.DrivingLicenseImagePath)
            .HasMaxLength(256);

        builder.HasIndex(x => x.Cnpj).IsUnique();
        builder.HasIndex(x => x.DrivingLicense).IsUnique();
    }
}
