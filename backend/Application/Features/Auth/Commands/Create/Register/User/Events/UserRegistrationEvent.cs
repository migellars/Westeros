using SharedKernel.Resources.CQRS;

namespace Application.Features.Auth.Commands.Create.Register.User.Events;

public abstract class UserRegistrationEvent: IWesterosNotification
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
}