using Microsoft.EntityFrameworkCore;

namespace MovieManager.Data.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> items, int pageIndex, int totalPage, int pageSize, int totalItems)
        {
            PageIndex = pageIndex;
            TotalPages = totalPage;
            PageSize = pageSize;
            TotalItems = totalItems;
            this.AddRange(items);
        }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }        
        public int TotalItems { get; set; }
        
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync (IQueryable<T> source, int pageIndex, int pageSize)
        {   
            var count = await source.CountAsync();
            var totalPage = (int) Math.Ceiling(count/ (double) pageSize);
            if ((pageIndex < 1) || (pageIndex > totalPage)) pageIndex = 1;            

            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, pageIndex, totalPage, pageSize, count);
        }
    }
}