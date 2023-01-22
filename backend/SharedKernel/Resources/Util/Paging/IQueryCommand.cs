namespace SharedKernel.Resources.Util.Paging
{
    public interface IQueryCommand<out TResult>
    {
        TResult Execute();
    }
}