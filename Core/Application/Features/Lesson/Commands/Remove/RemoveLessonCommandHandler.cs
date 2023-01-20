using Application.Contracts;
using Application.Helper.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Helpers.API_Helper;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Lesson.Commands.Remove;

internal class RemoveLessonCommandHandler: ICommandHandler<RemoveLessonCommand>
{
    private readonly ILannisterContext _context;

    public RemoveLessonCommandHandler(ILannisterContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(RemoveLessonCommand request, CancellationToken cancellationToken)
    {
        var validate = new RemoveLessonCommandValidator();
        await validate.ValidateAndThrowAsync(request, cancellationToken);
        // check if record is present
        if (_context.Tutorials == null) return ApiResponse.GetSuccess(null, null, BaseConstant.Deleted);
        var data = await _context.Tutorials.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDeleted == false,
            cancellationToken);
        switch (data)
        {
            case null:
                return ApiResponse.GetFailed();
            default:
                _context.Tutorials.Remove(data);
                await _context.SaveChangesAsync();
                //TODO: Notify User of Deactivation
                break;
        }

        return ApiResponse.GetSuccess(null, null, BaseConstant.Deleted);
    }
}