using Application.Contracts.IRepository;
using Application.Dto;
using Application.Helper;
using SharedKernel.Helpers.API_Helper;
using SharedKernel.Helpers.CQRS;
using SharedKernel.Helpers.Util.Paging;

namespace Application.Features.Auth.Queries.User.GetAll;

public class GetAllUserQueryHandler: IQueryHandler<Pagination<UserDto>, PagedList<UserDto>>
{
    private readonly IAuthRepository _authRepository;

    public GetAllUserQueryHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<ApiResponse<PagedList<UserDto>>> Handle(Pagination<UserDto> request, CancellationToken cancellationToken)
    {
        var result = await _authRepository.GetAllUser(request.Page, request.Size);
        return result == null ? new ApiResponse<PagedList<UserDto>>() : ApiResponse.GetSuccess(result);
    }
}