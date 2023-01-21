using SharedKernel.Resources.CQRS;

namespace Application.Features.Lesson.Commands.Create.Event;

public class LessonCreatedEvent: IWesterosNotification
{
    public LessonCreatedEvent(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}