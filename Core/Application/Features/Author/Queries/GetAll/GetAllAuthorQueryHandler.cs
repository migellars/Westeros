using Application.Contracts.IRepository;
using Application.Dto;
using Application.Helper;
using SharedKernel.Helpers.API_Helper;
using SharedKernel.Helpers.CQRS;
using SharedKernel.Helpers.Util.Paging;

namespace Application.Features.Author.Queries.GetAll;

public class GetAllAuthorQueryHandler: IQueryHandler<Pagination<AuthorDto>, PagedList<AuthorDto>>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAllAuthorQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<ApiResponse<PagedList<AuthorDto>>> Handle(Pagination<AuthorDto> request, CancellationToken cancellationToken)
    {
        var result = await _authorRepository.GetAllAuthor(request.Page, request.Size);
        return result == null ? new ApiResponse<PagedList<AuthorDto>>() : ApiResponse.GetSuccess( result);
    }
}