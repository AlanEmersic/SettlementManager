using System.ComponentModel.DataAnnotations;

namespace SettlementManager.Web.Models;

public sealed record AddSettlementModel
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Value for CountryId must be greater than 0")]
    public int CountryId { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed")]
    [MinLength(1)]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Special characters are not allowed")]
    [MinLength(1)]
    [MaxLength(15)]
    public string PostalCode { get; set; } = null!;
}