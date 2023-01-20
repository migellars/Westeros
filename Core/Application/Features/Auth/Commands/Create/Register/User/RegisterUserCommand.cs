using FluentValidation;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Auth.Commands.Create.Register.User;

public class RegisterUserCommand: ICommand
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
}

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.UserName).NotNull().MinimumLength(5);
    }
}