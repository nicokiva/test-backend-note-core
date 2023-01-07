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
            /*
            Validations:
            •	FullName: no puede ser vacio o null , no puede superar los 100 caracteres.
            •	IdNumber: no puede ser vacio o null , no puede superar los 10 caracteres y no se puede repetir.
            •	DateOfBirth: No puede ser en el futuro
            •	Company: Debe ser un Id de company valido.
            */

            if (string.IsNullOrEmpty(request.fullName) || request.fullName.Length > 100) {
                return ValidationResult.Fail(new FieldError(nameof(request.fullName), "Full name to large"));
            }

            if (string.IsNullOrEmpty(request.idNumber)) {
                return ValidationResult.Fail(new FieldError(nameof(request.idNumber), "Invalid idNumber, it is empty"));
            }

            var employee = await _unitOfWork.EmployeeRepository.EmployeeExistsByIdNumber(request.idNumber);
            if (employee) {
                return ValidationResult.Fail(new FieldError(nameof(request.idNumber), "Already exists"));
            }
            
            if (request.dateOfBirth > DateTime.Now) {
                return ValidationResult.Fail(new FieldError(nameof(request.dateOfBirth), "Invalid date"));
            }

            var company = await _unitOfWork.CompanyRepository.CompanyExistsByIdAsync(request.company);
            if (!company) {
                return ValidationResult.Fail(new FieldError(nameof(request.company), "Company does not exist"));
            }

            return  ValidationResult.Success;
        }
    }
}

