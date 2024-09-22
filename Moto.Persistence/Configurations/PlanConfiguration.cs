using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moto.Domain.Entities;

namespace Moto.Persistence.Configurations;

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
            new Plan(7, 30, 1.20M),
            new Plan(15, 28, 1.40M),
            new Plan(30, 22, 1),
            new Plan(45, 20, 1),
            new Plan(50, 18, 1)
        );
    }
}
