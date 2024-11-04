using Microsoft.EntityFrameworkCore;
using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Application.Settlements.Mappings;
using SettlementManager.Domain.Settlements;

namespace SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;

public sealed class SettlementPagedResponse
{
    public IReadOnlyList<SettlementDto> Settlements { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int PageCount { get; }
    public int TotalCount { get; }
    public bool HasNextPage { get; }
    public bool HasPreviousPage { get; }

    public SettlementPagedResponse(IReadOnlyList<SettlementDto> settlements, int pageNumber, int pageSize,
        int pageCount, int totalCount, bool hasNextPage, bool hasPreviousPage)
    {
        Settlements = settlements;
        PageNumber = pageNumber;
        PageSize = pageSize;
        PageCount = pageCount;
        TotalCount = totalCount;
        HasNextPage = hasNextPage;
        HasPreviousPage = hasPreviousPage;
    }

    public static async Task<SettlementPagedResponse> CreateAsync(IQueryable<Settlement> query, int pageNumber,
        int pageSize)
    {
        int totalCount = await query.CountAsync();
        int pageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        int page = pageNumber <= 1 ? 1 : pageNumber;
        bool hasNextPage = pageNumber * pageSize < totalCount;
        bool hasPreviousPage = pageNumber > 1;

        List<SettlementDto> settlements = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(x => x.Id)
            .Select(x => x.MapToDto())
            .ToListAsync();

        return new SettlementPagedResponse(settlements, pageNumber, pageSize, pageCount,
            totalCount, hasNextPage, hasPreviousPage);
    }
}