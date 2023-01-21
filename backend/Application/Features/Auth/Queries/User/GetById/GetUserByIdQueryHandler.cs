using Application.Contracts.IRepository;
using Application.Dto;
using SharedKernel.Resources.API_Helper;
using SharedKernel.Resources.Cache;
using SharedKernel.Resources.CQRS;

namespace Application.Features.Auth.Queries.User.GetById;

public class GetUserByIdQueryHandler: IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IAuthRepository  _authRepository;
    private readonly ICacheManager _cacheManager;

    public GetUserByIdQueryHandler(IAuthRepository authRepository, ICacheManager cacheManager)
    {
        _authRepository = authRepository;
        _cacheManager = cacheManager;
    }

    public async Task<ApiResponse<UserDto?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"user-profile-{request.Id}";
        var userDto =  _cacheManager.Get(cacheKey) as UserDto;
        switch (userDto)
        {
            case null:
                var result = await _authRepository.GetUser(request.Id);
                if (result == null) return ApiResponse.GetFailed<UserDto>();
                _cacheManager.Add(cacheKey, result);
                return ApiResponse.GetSuccess(result, null);
            default:
                return ApiResponse.GetSuccess(userDto, null);
        }
    }
}