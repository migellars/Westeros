using Application.Contracts;
using Application.Contracts.IRepository;
using Application.Dto;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Resources.Util.Paging;

namespace Infrastructure.Contracts.Repository;

public class LessonRepository: ILessonRepository
{
    private readonly IWesterosContext _context;

    public LessonRepository(IWesterosContext context)
    {
        _context = context;
    }

    public async Task<PagedList<TutorialDto>> GetAllLesson(int pageNumber, int pageSize)
    {
        var result = from lesson in _context.Tutorials
            where lesson.IsActive && lesson.IsDeleted == false
            select new TutorialDto()
            {
                Description = lesson.Description,
                Name = lesson.Name,
                Status = lesson.Status,
                Id = lesson.Id,
            };
        return await result.AsNoTracking().ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task<TutorialDto?> GetLesson(Guid lessonId)
    {
        var result = from lesson in _context.Tutorials
            where lesson.IsActive && lesson.IsDeleted == false && lesson.Id == lessonId 
            select new TutorialDto()
            {
                Description = lesson.Description,
                Name = lesson.Name,
                Status = lesson.Status,
                Id = lesson.Id,
            };
        return await result.AsNoTracking().FirstOrDefaultAsync();
    }
}