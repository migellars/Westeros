using Application.Contracts;
using Application.Contracts.IRepository;
using Application.Dto;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Helpers.Util.Paging;

namespace Infrastructure.Contracts.Repository;

public class AuthorRepository: IAuthorRepository
{
    private readonly ILannisterContext _context;

    public AuthorRepository(ILannisterContext context)
    {
        _context = context;
    }

    public async Task<PagedList<AuthorDto>> GetAllAuthor(int pageNumber, int pageSize)
    {
        var result = from author in _context.Author
            where author.IsDeleted.Equals(false) && author.IsActive.Equals(true)
            select new AuthorDto()
            {
                Address = author.Address,
                Email = author.Email,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Phone = author.Phone,
                Id = author.Id
            };
        return await result.AsNoTracking().ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task<AuthorDto?> GetAuthor(Guid profileId)
    {
        var result = from author in _context.Author
            where author.IsDeleted.Equals(false) && author.IsActive.Equals(true) && author.Id == profileId
            select new AuthorDto()
            {
                Address = author.Address,
                Email = author.Email,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Phone = author.Phone,
                Id = author.Id
            };
        return await result.AsNoTracking().FirstOrDefaultAsync();
    }
}