using Atmoz.EmissionBreakdownApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EmissionBreakdownApi.DTOs
{
    public class EmissionBreakdownQueryParametersDTO
    {
        public string PageToken { get; set; } = String.Empty;
        public int PageSize { get; set; } = 25;
        public string SortField { get; set; } = String.Empty;
        public string CategoryId { get; set; } = String.Empty;
        public string SubCategoryId { get; set; } = String.Empty;

    }

    public class CreatedEmissionBreakdownRowDTO
    {
        public long RowId { get; set; }
        public EmissionBreakdownRowDTO Data { get; set; }
    }
}
