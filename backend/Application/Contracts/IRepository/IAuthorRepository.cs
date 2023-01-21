using Application.Dto;
using SharedKernel.Resources.Util.Paging;

namespace Application.Contracts.IRepository;

public interface IAuthorRepository
{
    Task<PagedList<AuthorDto>> GetAllAuthor(int pageNumber, int pageSize);
    Task<AuthorDto?> GetAuthor(Guid profileId);
}