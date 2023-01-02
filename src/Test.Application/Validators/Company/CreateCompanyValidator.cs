using Test.Application.Commands.Company;
using Test.Application.Validators.Base;
using Test.Repository;

namespace Test.Application.Validators.Company;

public class CreateCompanyValidator: IBusinessValidationHandler<CreateCompanyCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCompanyValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ValidationResult> Validate(CreateCompanyCommand request)
    {
        return await _unitOfWork.CompanyRepository.CompanyExistsAsync(request.Code)
            ? ValidationResult.Fail(new FieldError(nameof(request.Code), "Already exists"))
            : ValidationResult.Success;
    }
}