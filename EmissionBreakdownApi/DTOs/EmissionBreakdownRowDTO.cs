using Atmoz.EmissionBreakdownApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EmissionBreakdownApi.DTOs
{
    public class EmissionBreakdownRowDTO
    {
        public int Id { get; set; }
        public EmissionCategoryDTO Category { get; set; }
        public EmissionSubCategoryDTO SubCategory { get; set; }
        public double TonsOfCO2 { get; set; }
    }
}
