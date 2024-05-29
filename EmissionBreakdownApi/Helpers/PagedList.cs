using Microsoft.EntityFrameworkCore;

namespace EmissionBreakdownApi.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageToken, int pageSize)
        {
            PageToken = pageToken;
            PageSize = pageSize;
            AddRange(items);
        }

        public int PageToken { get; set; }
        public int PageSize { get; set; }
        public int SortField { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber,
            int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
