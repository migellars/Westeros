using Application.Dto;
using FluentValidation;
using SharedKernel.Resources.CQRS;

namespace Application.Features.Auth.Commands.Login;

public class LoginUserCommand: ICommand<TokenDto>
{
    public string UserName { get; set; }
    //public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Password).NotNull();
        RuleFor(x => x.UserName).NotNull().MinimumLength(4);
        //RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).NotEmpty();
    }
}