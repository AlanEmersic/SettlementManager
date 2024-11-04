using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SettlementManager.Domain.Settlements;

namespace SettlementManager.Infrastructure.Persistence.Database.Configurations;

internal sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.RowVersion)
            .IsRowVersion();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GetUtcDate()")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedOnUpdate();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasMany(x => x.Settlements)
            .WithOne(x => x.Country)
            .HasForeignKey(x => x.CountryId);
    }
}