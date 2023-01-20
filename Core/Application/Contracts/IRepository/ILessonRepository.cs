using Application.Dto;
using SharedKernel.Helpers.Util.Paging;

namespace Application.Contracts.IRepository;

public interface ILessonRepository
{
    Task<PagedList<TutorialDto>> GetAllLesson(int pageNumber, int pageSize);
    Task<TutorialDto?> GetLesson(Guid lessonId);
}