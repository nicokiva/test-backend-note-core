using Test.Application.DTOs.Company;
using Test.Application.Queries.Base;

namespace Test.Application.Queries.Employee;

public interface IEmployeeQueries: IQueriesCollection
{
    Task<QueryResponse<CompanyDTO>> GetEmployeeByIdAsync(Guid employeeId,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<QueryPaginatedResponseDTO<CompaniesDTO>> ListEmployees(
        CancellationToken cancellationToken = default(CancellationToken));
}