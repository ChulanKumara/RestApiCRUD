using System.ComponentModel.DataAnnotations;

namespace Atmoz.EmissionBreakdownApi.Models;

public class EmissionBreakdownQueryParameters
{
    // A non guessable token to retrieve the next page of results
    public string PageToken { get; set; } = String.Empty;

    // The amount of results to return in the next page
    [Range(1, 100)]
    public int PageSize { get; set; } = 25;
    // The field to sort on while returning results
    public string SortField { get; set; } = String.Empty;
    // The CategoryId to return data for, all other categories will be ignored
    public string CategoryId { get; set; } = String.Empty;
    // The SubCategoryId to return data for, all other subcategories will be ignored
    public string SubCategoryId { get; set; } = String.Empty;

    public EmissionBreakdownQueryParameters()
    {
    }
}

public class CreatedEmissionBreakdownRow 
{
    public long RowId { get; set; }
    public EmissionBreakdownRow Data { get; set; }

    public CreatedEmissionBreakdownRow(long rowId, EmissionBreakdownRow data)
    {
        RowId = rowId;
        Data = data;
    }
}