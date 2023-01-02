using System.Threading.Tasks;
using Moq;
using Test.Application.Commands.Company;
using Test.Application.Validators.Company;
using Test.Repository;
using Xunit;

namespace Test.Application.UnitTests;

public class UnitTest
{

    [Fact]
    public void CreateCompanyValidatorSucceededWhenCompanyDoesNotExist()
    {   
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.CompanyRepository.CompanyExistsAsync(It.IsAny<string>())).Returns(Task.FromResult(false));
        var createCompanyValidator = new CreateCompanyValidator(unitOfWorkMock.Object);

        var result = createCompanyValidator.Validate(GetRequestCommand("Rosario Central", "RC"));

        Assert.True(result.Result.IsSuccessful);
    }

    [Fact]
    public void CreateCompanyValidatorMustFailWhenCompanyExist()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.CompanyRepository.CompanyExistsAsync(It.IsAny<string>())).Returns(Task.FromResult(true));
        var createCompanyValidator = new CreateCompanyValidator(unitOfWorkMock.Object);

        var result = createCompanyValidator.Validate(GetRequestCommand("Rosario Central", "RC"));

        Assert.False(result.Result.IsSuccessful);
    }


    private CreateCompanyCommand GetRequestCommand(string name, string code)
    {
        return new CreateCompanyCommand(name, code);
    }

}