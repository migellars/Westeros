using System.Linq.Expressions;
using SharedKernel.Resources.Util.Paging;

namespace SharedKernel.Resources.Contracts.IRepositories;

public interface IRepositoryReadOnly<TEntity> where TEntity : class
{
    TEntity Get(int entityId);
    TEntity Get(Guid entityGuid);
    Task<TEntity> GetAsync(int entityId);
    Task<TEntity> GetAsync(Guid entityGuid);
    Task<TEntity> GetAsync(string entityGuid);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate);
    Task<PagedList<TEntity>> GetPagedAsync(int page, int size);
    Task<PagedList<TEntity>> GetPagedAsync(Expression<Func<TEntity, bool>> predicate, int page, int size);
    Task<int> GetDataCountAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);
}