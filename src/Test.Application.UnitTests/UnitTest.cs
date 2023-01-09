using System.Threading.Tasks;
using Moq;
using Test.Application.Commands.Company;
using Test.Application.Validators.Company;
using Test.Application.Commands.Employee;
using Test.Application.Validators.Employee;
using Test.Repository;
using Xunit;
using System;

namespace Test.Application.UnitTests;

public class UnitTest
{
    [Fact]
    public void CreateCompanyValidatorSucceededWhenCompanyDoesNotExist()
    {   
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.CompanyRepository.CompanyExistsAsync(It.IsAny<string>())).Returns(Task.FromResult(false));
        var createCompanyValidator = new CreateCompanyValidator(unitOfWorkMock.Object);

        var result = createCompanyValidator.Validate(GetCompanyRequestCommand("Rosario Central", "RC"));

        Assert.True(result.Result.IsSuccessful);
    }

    [Fact]
    public void CreateCompanyValidatorMustFailWhenCompanyExist()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.CompanyRepository.CompanyExistsAsync(It.IsAny<string>())).Returns(Task.FromResult(true));
        var createCompanyValidator = new CreateCompanyValidator(unitOfWorkMock.Object);

        var result = createCompanyValidator.Validate(GetCompanyRequestCommand("Rosario Central", "RC"));

        Assert.False(result.Result.IsSuccessful);
    }


    private CreateCompanyCommand GetCompanyRequestCommand(string name, string code)
    {
        return new CreateCompanyCommand(name, code);
    }

    [Fact]
    public void CreateEmployeeValidatorFailsBecauseFullNameIsEmpty()
    {   
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var createEmployeeValidator = new CreateEmployeeValidator(unitOfWorkMock.Object);

        var result = createEmployeeValidator.Validate(GetEmployeeRequestCommand("", "1234", DateTime.UtcNow, Guid.NewGuid()));

        Assert.False(result.Result.IsSuccessful);
        Assert.Equal(result.Result.Error, "Full name cannot be empty");
    }

    [Fact]
    public void CreateEmployeeValidatorFailsBecauseFullNameIsTooLong()
    {   
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var createEmployeeValidator = new CreateEmployeeValidator(unitOfWorkMock.Object);

        var result = createEmployeeValidator.Validate(GetEmployeeRequestCommand("vlrvRPyDHYDxJOEHMkRgvlrvRPyDHYDxJOEHMkRgvlrvRPyDHYDxJOEHMkRgvlrvRPyDHYDxJOEHMkRgvlrvRPyDHYDxJOEHMkRgvlrvRPyDHYDxJOEHMkRg", "1234", DateTime.UtcNow, Guid.NewGuid()));

        Assert.False(result.Result.IsSuccessful);
        Assert.Equal(result.Result.Error, "Full name too large");
    }

    [Fact]
    public void CreateEmployeeValidatorFailsBecauseIdNumberIsEmpty()
    {   
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var createEmployeeValidator = new CreateEmployeeValidator(unitOfWorkMock.Object);

        var result = createEmployeeValidator.Validate(GetEmployeeRequestCommand("vlrvRPyDHY", "", DateTime.UtcNow, Guid.NewGuid()));

        Assert.False(result.Result.IsSuccessful);
        Assert.Equal(result.Result.Error, "Invalid idNumber, it is empty");
    }

    [Fact]
    public void CreateEmployeeValidatorFailsBecauseEmployeeAlreadyExists()
    {   
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.EmployeeRepository.EmployeeExistsByIdNumber(It.IsAny<string>())).Returns(Task.FromResult(true));
        var createEmployeeValidator = new CreateEmployeeValidator(unitOfWorkMock.Object);

        var result = createEmployeeValidator.Validate(GetEmployeeRequestCommand("vlrvRPyDHY", "1234", DateTime.UtcNow, Guid.NewGuid()));

        Assert.False(result.Result.IsSuccessful);
        Assert.Equal(result.Result.Error, "Already exists");
    }

    [Fact]
    public void CreateEmployeeValidatorFailsBecauseDateOfBirthIsTheFuture()
    {   
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.EmployeeRepository.EmployeeExistsByIdNumber(It.IsAny<string>())).Returns(Task.FromResult(false));
        var createEmployeeValidator = new CreateEmployeeValidator(unitOfWorkMock.Object);

        var result = createEmployeeValidator.Validate(GetEmployeeRequestCommand("vlrvRPyDHY", "1234", DateTime.UtcNow.AddDays(1), Guid.NewGuid()));

        Assert.False(result.Result.IsSuccessful);
        Assert.Equal(result.Result.Error, "Invalid date");
    }

    [Fact]
    public void CreateEmployeeValidatorFailsBecauseCompanyDoesNotExist()
    {   
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.EmployeeRepository.EmployeeExistsByIdNumber(It.IsAny<string>())).Returns(Task.FromResult(false));
        unitOfWorkMock.Setup(x => x.CompanyRepository.CompanyExistsByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(false));
        var createEmployeeValidator = new CreateEmployeeValidator(unitOfWorkMock.Object);

        var result = createEmployeeValidator.Validate(GetEmployeeRequestCommand("vlrvRPyDHY", "1234", DateTime.UtcNow, Guid.NewGuid()));

        Assert.False(result.Result.IsSuccessful);
        Assert.Equal(result.Result.Error, "Company does not exist");
    }

    [Fact]
    public void CreateEmployeeValidatorSucceed()
    {   
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.EmployeeRepository.EmployeeExistsByIdNumber(It.IsAny<string>())).Returns(Task.FromResult(false));
        unitOfWorkMock.Setup(x => x.CompanyRepository.CompanyExistsByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));
        var createEmployeeValidator = new CreateEmployeeValidator(unitOfWorkMock.Object);

        var result = createEmployeeValidator.Validate(GetEmployeeRequestCommand("vlrvRPyDHY", "1234", DateTime.UtcNow, Guid.NewGuid()));

        Assert.True(result.Result.IsSuccessful);
    }

    private CreateEmployeeCommand GetEmployeeRequestCommand(string fullName, string idNumber, DateTime dateOfBirth, Guid company)
    {
        return new CreateEmployeeCommand(fullName, idNumber, dateOfBirth, company);
    }
    

}