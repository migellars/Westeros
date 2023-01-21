using Application.Dto;
using SharedKernel.Resources.Util.Paging;

namespace Application.Contracts.IRepository;

public interface ILessonRepository
{
    Task<PagedList<TutorialDto>> GetAllLesson(int pageNumber, int pageSize);
    Task<TutorialDto?> GetLesson(Guid lessonId);
}