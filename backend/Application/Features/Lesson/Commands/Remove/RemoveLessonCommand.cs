using FluentValidation;
using SharedKernel.Resources.CQRS;

namespace Application.Features.Lesson.Commands.Remove;

public class RemoveLessonCommand: ICommand
{
    public RemoveLessonCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class RemoveLessonCommandValidator : AbstractValidator<RemoveLessonCommand>
{
    public RemoveLessonCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}