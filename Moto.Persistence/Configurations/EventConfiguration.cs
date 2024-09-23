using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moto.Domain.Entities;
using Moto.Persistence.Extensions;

namespace Moto.Persistence.Configurations;

/// <summary>
/// Configures the entity properties and constraints for the Event entity.
/// Implements the <see cref="IEntityTypeConfiguration{Event}"/> interface, 
/// used by Entity Framework to configure the model via the Fluent API.
/// </summary>
internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.OccuredOn)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Data)
            .IsRequired()
            .HasColumnType("jsonb");
    }
}
