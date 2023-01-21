using Application.Contracts;
using Application.Features.Lesson.Commands.Create.Event.Email;
using Application.Helper.Constants;
using Application.Helper.Enum;
using Domain.Models.Lesson;
using FluentValidation;
using MediatR;
using Microsoft.Data.SqlClient;
using SharedKernel.Resources.API_Helper;
using SharedKernel.Resources.CQRS;
using SharedKernel.Resources.Logging;

namespace Application.Features.Lesson.Commands.Create;

internal class CreateLessonCommandHandler: ICommandHandler<CreateLessonCommand>
{
    private readonly IWesterosContext _context;
    private readonly IPublisher _publisher;

    public CreateLessonCommandHandler(IWesterosContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<ApiResponse> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var validate = new CreateLessonCommandValidator();
        await validate.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);
        await CreateLessonBuilder(request, cancellationToken);

        await _publisher.Publish(new LessonCreatedEmailEvent(Guid.NewGuid())
        {
            Body = "New Lesson Created"
        }, cancellationToken);
        return ApiResponse.GetSuccess(null, null,BaseConstant.Created);
    }


    private async Task CreateLessonBuilder(CreateLessonCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var data = new Tutorial();
            data.Description = command.Description;
            data.Name = command.Name;
            data.Status = StatusConstants.Pending.Id;
            data.AuthorId = command.AuthorId;
            await _context.Tutorials.AddAsync(data, cancellationToken);
            await _context.SaveChangesAsync();
        }
        catch (SqlException ex)
        {
            WesterosLogger.LogError(ErrorConstant.DATABASE_CREATE_ERROR, ex.Message);
        }
    }
}