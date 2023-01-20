using Application.Dto;
using SharedKernel.Helpers.API_Helper;
using SharedKernel.Helpers.Util.Paging;

namespace Application.Contracts.IService;

public interface IAuthorService
{
    Task<ApiResponse<PagedList<AuthorDto>>> GetAllAuthor(int pageNumber, int pageSize);
    Task<ApiResponse<AuthorDto>> GetAuthor(Guid authorId);
}