using SettlementManager.Domain.Common;

namespace SettlementManager.Domain.Settlements;

public sealed class Settlement : VersionedEntity
{
    public int CountryId { get; init; }
    public string Name { get; init; } = null!;
    public string PostalCode { get; init; } = null!;

    public Country Country { get; init; } = null!;
}