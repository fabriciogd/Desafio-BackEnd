using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moto.Domain.Entities;

namespace Moto.Persistence.Configurations;

/// <summary>
/// Configures the entity properties and constraints for the Plan entity.
/// Implements the <see cref="IEntityTypeConfiguration{Plan}"/> interface, 
/// used by Entity Framework to configure the model via the Fluent API.
/// </summary>
internal sealed class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Ignore(entity => entity.DomainEvents);

        builder.Property(x => x.CostPerDay).HasPrecision(5, 2);
        builder.Property(x => x.Fee).HasPrecision(5, 2);

        builder.HasData(
            new Plan(7, 30, 0.2M),
            new Plan(15, 28, 0.4M),
            new Plan(30, 22, 0),
            new Plan(45, 20, 0),
            new Plan(50, 18, 0)
        );
    }
}
