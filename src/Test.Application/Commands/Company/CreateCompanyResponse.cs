using Test.Application.Commands.Base;
using Test.Application.DTOs.Company;

namespace Test.Application.Commands.Company;

public record CreateCompanyResponse: CommandResponse<CompanyDTO> ;