using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SettlementManager.Domain.Settlements;
using SettlementManager.Infrastructure.Persistence.Database;

namespace SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetSettlements;

internal sealed class GetSettlementsQueryHandler : IRequestHandler<GetSettlementsQuery, ErrorOr<SettlementPagedResponse>>
{
    private readonly SettlementManagerDbContext dbContext;

    public GetSettlementsQueryHandler(SettlementManagerDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ErrorOr<SettlementPagedResponse>> Handle(GetSettlementsQuery query,
        CancellationToken cancellationToken)
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

        SettlementPagedResponse settlements = await SettlementPagedResponse.CreateAsync(settlementsQuery, query.PageNumber, query.PageSize);

        return settlements;
    }
}