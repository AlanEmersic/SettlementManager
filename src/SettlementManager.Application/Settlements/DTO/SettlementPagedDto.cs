namespace SettlementManager.Application.Settlements.DTO;

public sealed record SettlementPagedDto(
    IReadOnlyList<SettlementDto> Settlements,
    int PageNumber,
    int PageSize,
    int PageCount,
    int TotalCount,
    bool HasPreviousPage,
    bool HasNextPage);