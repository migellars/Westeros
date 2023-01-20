using Application.Features.Auth.Commands.Create.Register.User.Events.Email;
using SharedKernel.Helpers.CQRS;
using SharedKernel.Helpers.Logging;

namespace Application.Features.Auth.Commands.Remove.User.Event.Email;

public class UserRemovedEmailEventHandler<TUserRegistered> : ILannisterNotificationHandler<TUserRegistered> where TUserRegistered : UserRemovedEmailEvent
{
    public async Task Handle(TUserRegistered notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Lord of the Seven kingdoms :: {notification.Body}");
        
        LannisterLogger.LogInfo(notification.Body);
        return;
    }
}