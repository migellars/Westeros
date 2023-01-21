using Application.Contracts;
using Application.Helper.Constants;
using Application.Helper.Enum;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Resources.API_Helper;
using SharedKernel.Resources.CQRS;

namespace Application.Features.Lesson.Commands.Update;

internal class UpdateLessonCommandHandler: ICommandHandler<UpdateLessonCommand>
{
    private readonly IWesterosContext _context;

    public UpdateLessonCommandHandler(IWesterosContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var validate = new UpdateLessonCommandValidator();
        await validate.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

        var getData = await _context.Tutorials.FirstOrDefaultAsync(x => x.Id == request.Id && x.AuthorId == request.AuthorId, cancellationToken: cancellationToken);
        if (getData is null) return ApiResponse.GetFailed();

        getData.Description = request.Description;
        getData.Name = request.Name;
        getData.Status = StatusConstants.Published.Id;

        _context.Tutorials.Update(getData);
        await _context.SaveChangesAsync();
        return ApiResponse.GetSuccess(null, null, BaseConstant.Updated);
    }
}