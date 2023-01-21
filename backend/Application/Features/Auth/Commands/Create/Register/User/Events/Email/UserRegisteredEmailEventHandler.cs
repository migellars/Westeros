using SharedKernel.Resources.CQRS;
using SharedKernel.Resources.Logging;

namespace Application.Features.Auth.Commands.Create.Register.User.Events.Email;

public class UserRegisteredEmailEventHandler<TUserRegistered> : IWesterosNotificationHandler<TUserRegistered> where TUserRegistered : UserRegisteredEmailEvent
{

    public async Task Handle(TUserRegistered notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Send Email to Kingdoms");
        
        WesterosLogger.LogInfo(notification.Body);
        return;
    }
}