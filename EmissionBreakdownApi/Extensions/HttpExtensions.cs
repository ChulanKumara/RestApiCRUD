using EmissionBreakdownApi.Helpers;
using System.Text.Json;

namespace EmissionBreakdownApi.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int pageToken, int pageSize, int sortField, int categoryId, int subCategoryId)
        {
            var paginationHeader = new PaginationHeader(pageToken, pageSize, sortField, categoryId, subCategoryId);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
