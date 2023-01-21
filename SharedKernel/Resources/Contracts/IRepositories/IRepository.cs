namespace SharedKernel.Resources.Contracts.IRepositories;

public interface IRepository<TEntity> : IRepositoryReadOnly<TEntity>, IRepositoryWriteOnly<TEntity> where TEntity : class
{
}