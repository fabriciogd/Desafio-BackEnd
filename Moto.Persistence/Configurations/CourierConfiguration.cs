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

        builder.OwnsOne(x => x.Cnpj)
             .Property(x => x.Value)
             .HasColumnName("Cnpj")
             .IsRequired()
             .HasMaxLength(14);

        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.OwnsOne(x => x.DrivingLicense)
            .Property(x => x.Value)
            .HasColumnName("DrivingLicense")
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(x => x.DrivingLicenseType)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(x => x.DrivingLicenseImagePath)
            .HasMaxLength(256);

        builder.OwnsOne(x => x.Cnpj).HasIndex(x => x.Value).IsUnique();
        builder.OwnsOne(x => x.DrivingLicense).HasIndex(x => x.Value).IsUnique();
    }
}
