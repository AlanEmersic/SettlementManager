using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Application.Settlements.Mappings;
using SettlementManager.Infrastructure.Persistence.Database;

namespace SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetCountries;

internal sealed class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, ErrorOr<IReadOnlyList<CountryDto>>>
{
    private readonly SettlementManagerDbContext dbContext;

    public GetCountriesQueryHandler(SettlementManagerDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ErrorOr<IReadOnlyList<CountryDto>>> Handle(GetCountriesQuery query, CancellationToken cancellationToken)
    {
        List<CountryDto> countries = await dbContext
            .Countries
            .AsNoTracking()
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        return countries;
    }
}