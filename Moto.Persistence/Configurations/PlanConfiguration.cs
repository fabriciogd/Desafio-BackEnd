using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moto.Domain.Entities;
using Moto.Persistence.Extensions;

namespace Moto.Persistence.Configurations;

internal sealed class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.CostPerDay).HasPrecision(7, 5);
        builder.Property(x => x.Fee).HasPrecision(5, 4);

        builder.HasIndex(x => x.Days).IsUnique();

        builder.HasData(
            new Plan(1, 7, 30, 1.20M),
            new Plan(2, 15, 28, 1.40M),
            new Plan(3, 30, 22, 1),
            new Plan(4, 45, 20, 1),
            new Plan(5, 50, 18, 1)
        );
    }
}
