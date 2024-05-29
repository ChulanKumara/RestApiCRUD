using System.ComponentModel.DataAnnotations;

namespace Atmoz.EmissionBreakdownApi.Models;

public class EmissionBreakdownRow
{
    public int Id { get; set; }
    [Required]
    // The categorization of the emission
    public EmissionCategory Category { get; set; }

    // The subcategorization of the emission
    public EmissionSubCategory SubCategory { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    // The tons of CO2 corresponding to this emission
    public double TonsOfCO2 { get;set; }
}