using System.ComponentModel.DataAnnotations;

namespace Atmoz.EmissionBreakdownApi.Models;

public class EmissionCategory
{
    [Required]
    public string Id  { get;set; }
    [Required]
    public string Name { get;set; }

    public EmissionCategory(string id, string name)
    {
        Id = id;
        Name = name;
    }
}