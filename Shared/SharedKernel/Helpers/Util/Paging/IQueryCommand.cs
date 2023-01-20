namespace SharedKernel.Helpers.Util.Paging
{
    public interface IQueryCommand<out TResult>
    {
        TResult Execute();
    }
}