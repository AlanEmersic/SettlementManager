using SettlementManager.Domain.Common;

namespace SettlementManager.Domain.Settlements;

public sealed class Country : VersionedEntity
{
    public required string Name { get; init; }

    public IReadOnlyCollection<Settlement> Settlements { get; init; } = null!;
}