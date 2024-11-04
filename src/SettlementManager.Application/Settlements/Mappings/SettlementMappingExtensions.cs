using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Domain.Settlements;

namespace SettlementManager.Application.Settlements.Mappings;

public static class SettlementMappingExtensions
{
    public static SettlementDto MapToDto(this Settlement settlement)
    {
        return new SettlementDto(
            Id: settlement.Id,
            Name: settlement.Name,
            PostalCode: settlement.PostalCode,
            Country: settlement.Country.MapToDto());
    }

    public static CountryDto MapToDto(this Country country)
    {
        return new CountryDto(
            Id: country.Id,
            Name: country.Name);
    }
}