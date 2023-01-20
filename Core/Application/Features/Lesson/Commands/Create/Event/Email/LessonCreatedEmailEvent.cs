namespace Application.Features.Lesson.Commands.Create.Event.Email;

public class LessonCreatedEmailEvent: LessonCreatedEvent
{
    public LessonCreatedEmailEvent(Guid id) : base(id)
    {
    }

    public string Body { get; set; }
}