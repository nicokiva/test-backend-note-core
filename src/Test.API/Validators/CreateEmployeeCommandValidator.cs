using FluentValidation;
using Test.Application.Commands.Employee;

namespace Test.API.Validators
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(c => c.fullName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(c => c.idNumber)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(c => c.dateOfBirth).Must(c => c < DateTime.UtcNow)
                .WithMessage("Date Of Birth cannot be in the future");

            RuleFor(c => c.company).Must(c => Guid.TryParse(c.ToString(), out var _))
                .WithMessage("The company ID is in an incorrect format");

            RuleFor(c => c.fullName).Must(c => !c.Contains(' '))
                .WithMessage("Must not contain blanks");

            RuleFor(c => c.idNumber).Must(c => !c.Contains(' '))
                .WithMessage("Must not contain blanks");
        }
    }
}

