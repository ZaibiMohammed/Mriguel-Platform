using Microsoft.EntityFrameworkCore;

namespace AlloVoisinClone.Application.Common.Models
{
    /// <summary>
    /// Extensions for PaginatedList
    /// </summary>
    public static class PaginatedListExtensions
    {
        /// <summary>
        /// Creates a paginated list from a queryable
        /// </summary>
        public static async Task<PaginatedList<T>> PaginatedListAsync<T>(
            this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
