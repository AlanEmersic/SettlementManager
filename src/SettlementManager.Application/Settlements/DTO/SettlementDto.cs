namespace SettlementManager.Application.Settlements.DTO;

public sealed record SettlementDto(int Id, string Name, string PostalCode, CountryDto Country);