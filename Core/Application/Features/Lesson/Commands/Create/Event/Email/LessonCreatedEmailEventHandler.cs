using Application.Features.Author.Commands.Create.Event.Email;
using SharedKernel.Helpers.CQRS;
using SharedKernel.Helpers.Logging;

namespace Application.Features.Lesson.Commands.Create.Event.Email;

public class LessonCreatedEmailEventHandler<TUserRegistered> : ILannisterNotificationHandler<TUserRegistered> where TUserRegistered : LessonCreatedEmailEvent
{
    public async Task Handle(TUserRegistered notification, CancellationToken cancellationToken)
    {
        // Send Email
        LannisterLogger.LogInfo("Send Email Event Lesson Triggered");
        return;
    }
}