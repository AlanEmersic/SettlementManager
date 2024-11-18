using SettlementManager.Domain.Common;

namespace SettlementManager.Domain.Settlements;

public sealed class Settlement : VersionedEntity
{
    public int CountryId { get; init; }
    public required string Name { get; init; }
    public required string PostalCode { get; init; }

    public Country Country { get; init; } = null!;
}