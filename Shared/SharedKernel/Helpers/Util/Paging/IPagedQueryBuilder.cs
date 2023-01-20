using System.Linq.Expressions;

namespace SharedKernel.Helpers.Util.Paging
{
    public interface IPagedQueryBuilder<TEntity> : IQueryCommand<PagedList<TEntity>>
    {
        IPagedQueryBuilder<TEntity> Search(string searchTerm);
        IPagedQueryBuilder<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        IPagedQueryBuilder<TEntity> OrderByDescendending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IPagedQueryBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IPagedQueryBuilder<TEntity> Order<TKey>(Expression<Func<TEntity, TKey>> keySelector, string order);
    }
}