﻿using System.ComponentModel.DataAnnotations;

namespace SettlementManager.Application.Settlements.Requests;

public sealed record CreateSettlementRequest
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Value for CountryId must be greater than 0")]
    public int CountryId { get; init; }

    [Required]
    [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only letters are allowed")]
    [MinLength(1)]
    [MaxLength(100)]
    public string Name { get; init; } = null!;

    [Required]
    [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Special characters are not allowed")]
    [MinLength(1)]
    [MaxLength(15)]
    public string PostalCode { get; init; } = null!;

    public CreateSettlementRequest(int countryId, string name, string postalCode)
    {
        CountryId = countryId;
        Name = name;
        PostalCode = postalCode;
    }
}