using Test.Application.DTOs.Company;
using Test.Application.Queries.Base;

namespace Test.Application.Queries.Employee;

public interface IEmployeeQueries: IQueriesCollection
{
    Task<QueryResponse<EmployeeDTO>> GetEmployeeByIdAsync(Guid employeeId,
        CancellationToken cancellationToken = default(CancellationToken));
}