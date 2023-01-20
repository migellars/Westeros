namespace SharedKernel.Helpers.Contracts;

public interface IUnitOfWork : IDisposable
{
    void Commit();
    Task CompleteAsync();

    /// <summary>
    /// This will be rarely needed for saving to the DB synchronously
    /// </summary>
    void Complete();
}