using System.Linq.Expressions;

namespace SharedKernel.Helpers.Contracts.IServices
{
    public interface ISearchPredicate<TEntity, in TQuery>
    {
        Expression<Func<TEntity, bool>> Apply(TQuery query);
    }
}