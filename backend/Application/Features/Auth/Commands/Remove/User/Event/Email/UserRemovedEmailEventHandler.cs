using SharedKernel.Resources.CQRS;
using SharedKernel.Resources.Logging;

namespace Application.Features.Auth.Commands.Remove.User.Event.Email;

public class UserRemovedEmailEventHandler<TUserRegistered> : IWesterosNotificationHandler<TUserRegistered> where TUserRegistered : UserRemovedEmailEvent
{
    public async Task Handle(TUserRegistered notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Lord of the Seven kingdoms :: {notification.Body}");
        
        WesterosLogger.LogInfo(notification.Body);
        return;
    }
}