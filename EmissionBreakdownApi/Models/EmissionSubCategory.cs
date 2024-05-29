using System.ComponentModel.DataAnnotations;

namespace Atmoz.EmissionBreakdownApi.Models;

public class EmissionSubCategory
{
    [Required]
    public string Id  { get;set; }
    [Required]
    public string Name { get;set; }

    public EmissionSubCategory(string id, string name)
    {
        Id = id;
        Name = name;
    }
}