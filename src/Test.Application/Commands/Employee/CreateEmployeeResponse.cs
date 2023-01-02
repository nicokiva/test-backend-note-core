using Test.Application.Commands.Base;
using Test.Application.DTOs.Employee;

namespace Test.Application.Commands.Employee;

public record CreateEmployeeResponse: CommandResponse<EmployeeDTO> ;