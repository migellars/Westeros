using SharedKernel.Helpers.CQRS;

namespace Application.Features.Auth.Commands.Create.Register.User.Events;

public abstract class UserRegistrationEvent: ILannisterNotification
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
}