using System.ComponentModel.DataAnnotations;

namespace SettlementManager.Web.Models;

public sealed record EditSettlementModel
{
    [Required] public int Id { get; set; }

    [Required] public int CountryId { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Only letters are allowed")]
    [MinLength(1)]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Special characters are not allowed")]
    [MinLength(1)]
    [MaxLength(15)]
    public string PostalCode { get; set; } = null!;

    public EditSettlementModel(int id, string name, string postalCode, int countryId)
    {
        Id = id;
        Name = name;
        PostalCode = postalCode;
        CountryId = countryId;
    }
}