using FluentValidation;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Author.Commands.Remove;

public class RemoveAuthorCommand: ICommand
{
    public RemoveAuthorCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class RemoveAuthorCommandValidator : AbstractValidator<RemoveAuthorCommand>
{
    public RemoveAuthorCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}