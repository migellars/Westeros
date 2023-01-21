using SharedKernel.Resources.CQRS;

namespace Application.Features.Auth.Commands.Remove.User.Event;

public class UserRemovedEvent: IWesterosNotification
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
}