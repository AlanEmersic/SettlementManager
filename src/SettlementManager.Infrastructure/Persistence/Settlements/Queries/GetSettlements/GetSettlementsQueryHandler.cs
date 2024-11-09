using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Domain.Settlements;
using SettlementManager.Infrastructure.Persistence.Database;

namespace SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;

internal sealed class GetSettlementsQueryHandler : IRequestHandler<GetSettlementsQuery, ErrorOr<SettlementPagedDto>>
{
    private readonly SettlementManagerDbContext dbContext;

    public GetSettlementsQueryHandler(SettlementManagerDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ErrorOr<SettlementPagedDto>> Handle(GetSettlementsQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Settlement> settlementsQuery = dbContext
            .Settlements
            .Include(x => x.Country)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            settlementsQuery = settlementsQuery
                .Where(x => x.Name.Contains(query.Search));
        }

        SettlementPagedDto settlements = await SettlementPagedResponse.CreateAsync(settlementsQuery, query.PageNumber, query.PageSize, cancellationToken);

        return settlements;
    }
}