using Test.Application.Commands.Employee;
using Test.Application.Validators.Base;
using Test.Repository;

namespace Test.Application.Validators.Employee
{
    public class CreateEmployeeValidator : IBusinessValidationHandler<CreateEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidationResult> Validate(CreateEmployeeCommand request)
        {
            return await _unitOfWork.EmployeeRepository.EmployeeExistsByIdNumber(request.idNumber)
                    ? ValidationResult.Fail(new FieldError(nameof(request.idNumber), "Already exists"))
                    : ValidationResult.Success; ;
        }
    }
}

