using Dapper;
using Microsoft.Extensions.Logging;
using Test.Application.DTOs.Company;
using Test.Application.Queries.Base;

namespace Test.Application.Queries.Employee;

public class EmployeeQueries: QueryRunner, IEmployeeQueries
{

    public EmployeeQueries(QueryConnectionString connectionString, ILogger<EmployeeQueries> logger): 
        base(connectionString, logger) { }
    
    public async Task<QueryResponse<EmployeeDTO>> GetEmployeeByIdAsync(Guid employeeId, CancellationToken cancellationToken = default(CancellationToken))
    {
        var company = await RunQueryFirstAsync(
            @"SELECT id, code, name, active, created_at
                     FROM company
                    WHERE id = @id",
            new
            {
                id = employeeId
            }, cancellationToken);
        
        if (company == null)
            return QueryResponse<EmployeeDTO>.NotFound();
            
        return QueryResponse<EmployeeDTO>.Success(
            MapToCompanyDTO(company));
    }

    private EmployeeDTO MapToCompanyDTO(dynamic row)
    {
        return new EmployeeDTO()
        {
            Id = row.id,
            Code = row.code,
            Name = row.name,
            Active = row.active,
            CreatedAt = row.created_at
        };
    }
}