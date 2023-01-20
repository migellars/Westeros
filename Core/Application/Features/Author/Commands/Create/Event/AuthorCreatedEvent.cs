using SharedKernel.Helpers.CQRS;

namespace Application.Features.Author.Commands.Create.Event;

public class AuthorCreatedEvent: ILannisterNotification
{
    public AuthorCreatedEvent(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}