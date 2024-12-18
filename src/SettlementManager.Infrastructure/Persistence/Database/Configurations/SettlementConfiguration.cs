﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SettlementManager.Domain.Settlements;

namespace SettlementManager.Infrastructure.Persistence.Database.Configurations;

internal sealed class SettlementConfiguration : IEntityTypeConfiguration<Settlement>
{
    public void Configure(EntityTypeBuilder<Settlement> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.RowVersion)
            .IsRowVersion();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GetUtcDate()")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PostalCode)
            .IsRequired()
            .HasMaxLength(15);

        builder
            .HasOne(x => x.Country)
            .WithMany(x => x.Settlements)
            .HasForeignKey(x => x.CountryId);

        builder.ToTable(tb => tb.HasTrigger("SetSettlementUpdatedAtOnUpdate"));
    }
}