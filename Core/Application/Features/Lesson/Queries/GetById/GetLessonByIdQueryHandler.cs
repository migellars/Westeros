using Application.Contracts.IRepository;
using Application.Dto;
using SharedKernel.Helpers.API_Helper;
using SharedKernel.Helpers.Cache;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Lesson.Queries.GetById;

public class GetLessonByIdQueryHandler: IQueryHandler<GetLessonByIdQuery, TutorialDto>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly ICacheManager _cacheManager;

    public GetLessonByIdQueryHandler(ILessonRepository lessonRepository, ICacheManager cacheManager)
    {
        _lessonRepository = lessonRepository;
        _cacheManager = cacheManager;
    }

    public async Task<ApiResponse<TutorialDto>> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"LessonId-{request.Id}";
        var tutorialDto =  _cacheManager.Get(cacheKey) as TutorialDto;
        switch (tutorialDto)
        {
            case null:
                var result = await _lessonRepository.GetLesson(request.Id);
                if (result == null) return ApiResponse.GetFailed<TutorialDto>();
                _cacheManager.Add(cacheKey, result);
                return ApiResponse.GetSuccess(result);
            default:
                return ApiResponse.GetSuccess(tutorialDto);
        }
    }
}