using Application.Contracts;
using Application.Contracts.IRepository;
using Application.Dto;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Helpers.Util.Paging;

namespace Infrastructure.Contracts.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly LannisterContext _context;

    public AuthRepository(LannisterContext context)
    {
        _context = context;
    }

    public async Task<PagedList<UserDto>> GetAllUser(int pageNumber, int pageSize)
    {
        var result = from user in _context.Users
                     select new UserDto()
                     {
                         Email = user.Email,
                         UserName = user.UserName,
                         Id = user.Id
                     };
        return await result.AsNoTracking().ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task<UserDto?> GetUser(Guid profileId)
    {
        var result = from user in _context.Users
                     where user.Id == profileId
                     select new UserDto()
                     {
                         Email = user.Email,
                         UserName = user.UserName,
                         Id = user.Id
                     };
        return await result.AsNoTracking().FirstOrDefaultAsync();
    }
}