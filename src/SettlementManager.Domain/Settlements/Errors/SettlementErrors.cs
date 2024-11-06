using ErrorOr;

namespace SettlementManager.Domain.Settlements.Errors;

public static class SettlementErrors
{
    public static readonly Error SettlementAlreadyCreated = Error.Conflict(
        code: SettlementErrorCodes.SettlementAlreadyCreated,
        description: "Settlement already created.");

    public static readonly Error SettlementNotFound = Error.NotFound(
        code: SettlementErrorCodes.SettlementNotFound,
        description: "Settlement not found.");
}