using SettlementManager.Domain.Common;

namespace SettlementManager.Domain.Settlements;

public sealed class Country : VersionedEntity
{
    public string Name { get; init; } = null!;

    public IReadOnlyCollection<Settlement> Settlements { get; init; } = null!;
}