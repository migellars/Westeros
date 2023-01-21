namespace Application.Features.Auth.Commands.Remove.User.Event.Email;

public class UserRemovedEmailEvent: UserRemovedEvent
{
    public string Body { get; set; }

}