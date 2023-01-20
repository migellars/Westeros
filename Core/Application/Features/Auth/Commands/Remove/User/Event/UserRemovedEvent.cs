using SharedKernel.Helpers.CQRS;

namespace Application.Features.Auth.Commands.Remove.User.Event;

public class UserRemovedEvent: ILannisterNotification
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
}