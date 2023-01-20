namespace SharedKernel.Helpers.Contracts.IRepositories;

public interface IRepositoryWriteOnly<in TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}