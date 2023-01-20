using SharedKernel.Helpers.CQRS;

namespace Application.Features.Lesson.Commands.Create.Event;

public class LessonCreatedEvent: ILannisterNotification
{
    public LessonCreatedEvent(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}