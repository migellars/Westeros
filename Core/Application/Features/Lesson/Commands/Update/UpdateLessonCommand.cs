using FluentValidation;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Lesson.Commands.Update;

public class UpdateLessonCommand: ICommand
{
    public UpdateLessonCommand(Guid id, string name, string description, Guid author)
    {
        Id = id;
        Name = name;
        Description = description;
        AuthorId = author;
    }

    public UpdateLessonCommand()
    {
        
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
}

public class UpdateLessonCommandValidator : AbstractValidator<UpdateLessonCommand>
{
    public UpdateLessonCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().NotNull();
        RuleFor(x=>x.Name).NotEmpty().NotNull();
        RuleFor(x=>x.AuthorId).NotEmpty().NotNull();
    }
}