using SharedKernel.Helpers.CQRS;
using SharedKernel.Helpers.Logging;

namespace Application.Features.Auth.Commands.Create.Register.User.Events.Email;

public class UserRegisteredEmailEventHandler<TUserRegistered> : ILannisterNotificationHandler<TUserRegistered> where TUserRegistered : UserRegisteredEmailEvent
{

    public async Task Handle(TUserRegistered notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Send Email to Kingdoms");
        
        LannisterLogger.LogInfo(notification.Body);
        return;
    }
}