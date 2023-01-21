using Application.Dto;
using SharedKernel.Resources.API_Helper;
using SharedKernel.Resources.Util.Paging;

namespace Application.Contracts.IService;

public interface IAuthorService
{
    Task<ApiResponse<PagedList<AuthorDto>>> GetAllAuthor(int pageNumber, int pageSize);
    Task<ApiResponse<AuthorDto>> GetAuthor(Guid authorId);
}