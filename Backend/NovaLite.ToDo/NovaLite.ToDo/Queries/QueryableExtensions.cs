using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Dto;

namespace NovaLite.ToDo.Queries
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResponse<T>> ToPage<T>(this IQueryable<T> source, int page, int size)
        {
            if (page <= 0)
            {
                page = 1;
            }
            var items = await source.Skip((page - 1) * size).Take(size).ToListAsync();
            var total = await source.CountAsync();
            return new PagedResponse<T> { Items = items, Total = total };
        }
    }
}
