using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moto.Domain.Entities;
using Moto.Persistence.Extensions;

namespace Moto.Persistence.Configurations;

internal sealed class CourierConfiguration : IEntityTypeConfiguration<Courier>
{
    public void Configure(EntityTypeBuilder<Courier> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.Identificador)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.CNPJ)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(x => x.DataNascimento)
            .IsRequired();

        builder.Property(x => x.NumeroCNH)
            .IsRequired()
            .HasMaxLength(9);

        builder.Property(x => x.TipoCNH)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(x => x.ImagemCNH)
            .HasMaxLength(256);

        builder.HasIndex(x => x.CNPJ).IsUnique();
        builder.HasIndex(x => x.NumeroCNH).IsUnique();
    }
}
