using Test.Application.Commands.Base;
using Test.Application.Queries.Base;

namespace Test.Application.DTOs.Employee
{
    public class EmployeeDTO : ICommandDataResponse, IQueryDataResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string IdNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Guid Company { get; set; }
    }
}

