using ErrorOr;
using MediatR;
using SettlementManager.Application.Settlements.DTO;

namespace SettlementManager.Infrastructure.Persistence.Settlements.Queries.GetCountries;

public sealed record GetCountriesQuery : IRequest<ErrorOr<IReadOnlyList<CountryDto>>>;