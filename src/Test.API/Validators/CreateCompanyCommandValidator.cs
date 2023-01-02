using FluentValidation;
using Test.Application.Commands.Company;

namespace Test.API.validators;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.Code)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(c => c.Code).Must(c => !c.Contains(' '))
            .WithMessage("Must not contain blanks");
    }
}