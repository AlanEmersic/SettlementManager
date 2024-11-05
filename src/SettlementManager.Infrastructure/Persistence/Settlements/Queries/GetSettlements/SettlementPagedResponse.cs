using Microsoft.EntityFrameworkCore;
using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Application.Settlements.Mappings;
using SettlementManager.Domain.Settlements;

namespace SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;

public sealed class SettlementPagedResponse
{
    public IReadOnlyList<SettlementDto> Settlements { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int PageCount { get; init; }
    public int TotalCount { get; init; }
    public bool HasNextPage { get; init; }
    public bool HasPreviousPage { get; init; }

    public SettlementPagedResponse(IReadOnlyList<SettlementDto> settlements, int pageNumber, int pageSize, int pageCount, int totalCount, bool hasNextPage, bool hasPreviousPage)
    {
        Settlements = settlements;
        PageNumber = pageNumber;
        PageSize = pageSize;
        PageCount = pageCount;
        TotalCount = totalCount;
        HasNextPage = hasNextPage;
        HasPreviousPage = hasPreviousPage;
    }

    public static async Task<SettlementPagedResponse> CreateAsync(IQueryable<Settlement> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        int totalCount = await query.CountAsync(cancellationToken);
        int pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        int page = pageNumber <= 1 ? 1 : pageNumber;
        bool hasNextPage = pageNumber * pageSize < totalCount;
        bool hasPreviousPage = pageNumber > 1;

        List<SettlementDto> settlements = await query
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        return new SettlementPagedResponse(settlements, pageNumber, pageSize, pageCount, totalCount, hasNextPage, hasPreviousPage);
    }
}