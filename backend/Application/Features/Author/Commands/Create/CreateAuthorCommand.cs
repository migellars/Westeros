using Domain.Models.Helper.ValueObject;
using FluentValidation;
using System.Text.RegularExpressions;
using SharedKernel.Resources.CQRS;

namespace Application.Features.Author.Commands.Create;

public class CreateAuthorCommand: ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string LocalGovt { get; set; }
    public string ZipCode { get; set; }
    private Address Address { get; set; }

    public Address GetAddress()
    {
        return new Address(Street, City, State, Country, ZipCode, LocalGovt);
    }
}

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty();
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x=>x.Email).EmailAddress().NotEmpty().NotNull().WithMessage("Email is required");
        RuleFor(x=>x.Phone).NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(14).WithMessage("PhoneNumber must not exceed 50 characters.").Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}))?\d{3}\d{4}")).WithMessage("Phone Number not valid");
    }
}