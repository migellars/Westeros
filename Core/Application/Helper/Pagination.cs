using SharedKernel.Helpers.CQRS;
using SharedKernel.Helpers.Util.Paging;

namespace Application.Helper;

public class Pagination<T>: IQuery<PagedList<T>> where T : class
{
    public Pagination(int pageNumber, int pageSize, string search = "")
    {
        Page = pageNumber;
        Size = pageSize;
        SearchTerm = search;
    }

    public Pagination()
    {
        
    }
    public int Count { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public string SearchTerm { get; set; }
}