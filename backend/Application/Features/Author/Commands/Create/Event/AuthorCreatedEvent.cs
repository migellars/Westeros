using SharedKernel.Resources.CQRS;

namespace Application.Features.Author.Commands.Create.Event;

public class AuthorCreatedEvent: IWesterosNotification
{
    public AuthorCreatedEvent(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}