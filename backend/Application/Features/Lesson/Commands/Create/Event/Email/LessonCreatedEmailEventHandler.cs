using SharedKernel.Resources.CQRS;
using SharedKernel.Resources.Logging;

namespace Application.Features.Lesson.Commands.Create.Event.Email;

public class LessonCreatedEmailEventHandler<TUserRegistered> : IWesterosNotificationHandler<TUserRegistered> where TUserRegistered : LessonCreatedEmailEvent
{
    public async Task Handle(TUserRegistered notification, CancellationToken cancellationToken)
    {
        // Send Email
        WesterosLogger.LogInfo("Send Email Event Lesson Triggered");
        return;
    }
}