using Application.Contracts.IRepository;
using Application.Dto;
using SharedKernel.Helpers.API_Helper;
using SharedKernel.Helpers.Cache;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Author.Queries.GetById;

public class GetAuthorByIdQueryHandler: IQueryHandler<GetAuthorByIdQuery, AuthorDto>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ICacheManager _cacheManager;

    public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository, ICacheManager cacheManager)
    {
        _authorRepository = authorRepository;
        _cacheManager = cacheManager;
    }

    public async Task<ApiResponse<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"AuthorId-{request.Id}";
       var authorDto = _cacheManager.Get(cacheKey) as AuthorDto;
        switch (authorDto)
        {
            case null:
                var result = await _authorRepository.GetAuthor(request.Id);
                if (result == null) return ApiResponse.GetFailed<AuthorDto>();
                _cacheManager.Add(cacheKey, result);
                return ApiResponse.GetSuccess(result);
            default:
                return ApiResponse.GetSuccess(authorDto);
        }
    }
}