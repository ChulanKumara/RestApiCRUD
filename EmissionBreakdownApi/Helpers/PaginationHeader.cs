using System.Drawing.Printing;

namespace EmissionBreakdownApi.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int pageToken, int pageSize, int sortField, int categoryId, int subCategoryId)
        {
            PageToken = pageToken;
            PageSize = pageSize;
            SortField = sortField;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
        }

        public int PageToken { get; set; }
        public int PageSize { get; set; }
        public int SortField { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
    }
}
