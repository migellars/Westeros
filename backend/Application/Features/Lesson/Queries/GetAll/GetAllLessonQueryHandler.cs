using Application.Contracts.IRepository;
using Application.Dto;
using Application.Helper;
using SharedKernel.Resources.API_Helper;
using SharedKernel.Resources.CQRS;
using SharedKernel.Resources.Util.Paging;

namespace Application.Features.Lesson.Queries.GetAll;

public class GetAllLessonQueryHandler: IQueryHandler<Pagination<TutorialDto>, PagedList<TutorialDto>>
{
    private readonly ILessonRepository _lessonRepository;

    public GetAllLessonQueryHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<ApiResponse<PagedList<TutorialDto>>> Handle(Pagination<TutorialDto> request, CancellationToken cancellationToken)
    {
        var result = await _lessonRepository.GetAllLesson(request.Page, request.Size);
        return result == null ? new ApiResponse<PagedList<TutorialDto>>() : ApiResponse.GetSuccess( result);
    }
}