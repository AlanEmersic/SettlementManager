using Microsoft.EntityFrameworkCore;
using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Application.Settlements.Mappings;
using SettlementManager.Domain.Settlements;

namespace SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;

internal static class SettlementPagedResponse
{
    public static async Task<SettlementPagedDto> CreateAsync(IQueryable<Settlement> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
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

        return new SettlementPagedDto(settlements, pageNumber, pageSize, pageCount, totalCount, hasPreviousPage, hasNextPage);
    }
}