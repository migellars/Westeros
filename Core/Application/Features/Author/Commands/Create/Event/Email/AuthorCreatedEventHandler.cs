using SharedKernel.Helpers.CQRS;
using SharedKernel.Helpers.Logging;

namespace Application.Features.Author.Commands.Create.Event.Email;

public class AuthorCreatedEventEmailHandler<TUserRegistered> : ILannisterNotificationHandler<TUserRegistered> where TUserRegistered : AuthorCreatedEmailEvent
{
    public async Task Handle(TUserRegistered notification, CancellationToken cancellationToken)
    {
        // Send Email
        LannisterLogger.LogInfo("Send Email Event Triggered");
        return;
    }
}