using Test.Application.Commands.Base;
using Test.Application.Queries.Base;

namespace Test.Application.DTOs.Company;

public record CompanyDTO: ICommandDataResponse, IQueryDataResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool Active { get; set; }
}