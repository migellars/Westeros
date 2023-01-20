using FluentValidation;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Lesson.Commands.Create;

public class CreateLessonCommand: ICommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
}

public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
{
    public CreateLessonCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().NotNull();
        RuleFor(x=>x.Name).NotEmpty().NotNull();
        RuleFor(x=>x.AuthorId).NotEmpty().NotNull().WithMessage("Provide Author Id");
    }
}