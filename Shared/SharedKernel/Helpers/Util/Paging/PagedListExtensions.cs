using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Helpers.Util.Paging
{
    public static class PagedListExtensions
    {
        public static async Task<PagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> entities, int page, int size) where TEntity : class
        {
            var count = await entities.CountAsync();

            if (entities.GetType() != typeof(IOrderedQueryable<>))
            {
                entities = entities.OrderByDescending(e => true);
            }
            var paged = new PagedList<TEntity>(page, size)
            {
                Items = entities.Skip((page - 1) * page)
                    .Take(size == 0 ? count : size),
                Count = count,
                Page = page,
                Size = size == 0 ? count : size
            };
            return paged;
        }
        public static async Task<PagedList<TEntity>> ToPagedListEnumerableAsync<TEntity>(this IEnumerable<TEntity> entities, int page, int size) where TEntity : class
        {
            var count = entities.Count();

            if (entities.GetType() != typeof(IOrderedEnumerable<>))
            {
                entities = entities.OrderByDescending(e => true);
            }
            var paged = new PagedList<TEntity>(page, size)
            {
                Data = entities.Skip((page - 1) * page)
                    .Take(size == 0 ? count : size),
                Count = count,
                Page = page,
                Size = size == 0 ? count : size
            };
            return paged;
        }
    }
}
