namespace SettlementManager.Domain.Common;

public abstract class VersionedEntity : Entity
{
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
    public byte[] RowVersion { get; init; } = [];
}