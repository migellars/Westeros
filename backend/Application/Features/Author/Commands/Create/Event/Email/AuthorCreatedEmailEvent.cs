namespace Application.Features.Author.Commands.Create.Event.Email;

public class AuthorCreatedEmailEvent: AuthorCreatedEvent
{
    private string Body { get; set; }

    public AuthorCreatedEmailEvent(Guid id, string body) : base(id)
    {
        Body = body;
        Id = id;
    }
}