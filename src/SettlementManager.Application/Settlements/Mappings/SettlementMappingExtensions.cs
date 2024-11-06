using SettlementManager.Application.Settlements.Commands.CreateSettlement;
using SettlementManager.Application.Settlements.Commands.UpdateSettlementCommand;
using SettlementManager.Application.Settlements.DTO;
using SettlementManager.Application.Settlements.Requests;
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

    public static Settlement MapToDomain(this CreateSettlementCommand command)
    {
        return new Settlement { CountryId = command.CountryId, Name = command.Name, PostalCode = command.PostalCode };
    }

    public static Settlement MapToDomain(this UpdateSettlementCommand command)
    {
        return new Settlement { Id = command.Id, CountryId = command.CountryId, Name = command.Name, PostalCode = command.PostalCode };
    }

    public static Settlement MapToDomain(this Settlement settlement, Settlement currentSettlement)
    {
        return new Settlement
        {
            Id = currentSettlement.Id,
            CountryId = settlement.CountryId,
            Name = settlement.Name,
            PostalCode = settlement.PostalCode,
            RowVersion = currentSettlement.RowVersion
        };
    }

    public static CreateSettlementCommand MapToCommand(this CreateSettlementRequest request)
    {
        return new CreateSettlementCommand(
            CountryId: request.CountryId,
            Name: request.Name,
            PostalCode: request.PostalCode);
    }

    public static UpdateSettlementCommand MapToCommand(this UpdateSettlementRequest request)
    {
        return new UpdateSettlementCommand(
            Id: request.Id,
            CountryId: request.CountryId,
            Name: request.Name,
            PostalCode: request.PostalCode);
    }
}