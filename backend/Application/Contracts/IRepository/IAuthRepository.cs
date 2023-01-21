using Application.Dto;
using SharedKernel.Resources.Util.Paging;

namespace Application.Contracts.IRepository;

public interface IAuthRepository
{
    Task<PagedList<UserDto>> GetAllUser(int pageNumber, int pageSize);
    Task<UserDto?> GetUser(Guid profileId);
}