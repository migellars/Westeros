using SharedKernel.Resources.CQRS;
using SharedKernel.Resources.Logging;

namespace Application.Features.Author.Commands.Create.Event.Email;

public class AuthorCreatedEventEmailHandler<TUserRegistered> : IWesterosNotificationHandler<TUserRegistered> where TUserRegistered : AuthorCreatedEmailEvent
{
    public async Task Handle(TUserRegistered notification, CancellationToken cancellationToken)
    {
        // Send Email
        WesterosLogger.LogInfo("Send Email Event Triggered");
        return;
    }
}