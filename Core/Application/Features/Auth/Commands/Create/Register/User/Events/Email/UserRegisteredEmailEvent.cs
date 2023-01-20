namespace Application.Features.Auth.Commands.Create.Register.User.Events.Email;

public class UserRegisteredEmailEvent : UserRegistrationEvent
{
    public string Body { get; set; }
}