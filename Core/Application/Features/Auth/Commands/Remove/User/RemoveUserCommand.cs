using FluentValidation;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Auth.Commands.Remove.User;

public class RemoveUserCommand: ICommand
{
    public RemoveUserCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
{
    public RemoveUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}