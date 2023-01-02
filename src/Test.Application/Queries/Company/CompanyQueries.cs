using Dapper;
using Microsoft.Extensions.Logging;
using Test.Application.DTOs.Company;
using Test.Application.Queries.Base;

namespace Test.Application.Queries.Company;

public class CompanyQueries: QueryRunner, ICompanyQueries
{

    public CompanyQueries(QueryConnectionString connectionString, ILogger<CompanyQueries> logger): 
        base(connectionString, logger) { }
    
    public async Task<QueryResponse<CompanyDTO>> GetCompanyByIdAsync(Guid companyId, CancellationToken cancellationToken = default(CancellationToken))
    {
        var company = await RunQueryFirstAsync(
            @"SELECT id, code, name, active, created_at
                     FROM company
                    WHERE id = @id",
            new
            {
                id = companyId
            }, cancellationToken);
        
        if (company == null)
            return QueryResponse<CompanyDTO>.NotFound();
            
        return QueryResponse<CompanyDTO>.Success(
            MapToCompanyDTO(company));
    }

    private CompanyDTO MapToCompanyDTO(dynamic row)
    {
        return new CompanyDTO()
        {
            Id = row.id,
            Code = row.code,
            Name = row.name,
            Active = row.active,
            CreatedAt = row.created_at
        };
    }

    public async Task<QueryPaginatedResponseDTO<CompaniesDTO>> ListCompanies(
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var companies = await RunQueryAsync(
            @"SELECT id, code, name, active, created_at
                     FROM company", cancellationToken);

        return new QueryPaginatedResponseDTO<CompaniesDTO>(
            new CompaniesDTO(companies.AsList().Select<dynamic, CompanyDTO>(c => MapToCompanyDTO(c))
                .ToList<CompanyDTO>()),
            10, 1, 1);
    }
}