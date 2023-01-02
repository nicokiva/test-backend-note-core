using Test.Application.DTOs.Company;
using Test.Application.Queries.Base;

namespace Test.Application.Queries.Company;

public interface ICompanyQueries: IQueriesCollection
{
    Task<QueryResponse<CompanyDTO>> GetCompanyByIdAsync(Guid companyId,
        CancellationToken cancellationToken = default(CancellationToken));

    Task<QueryPaginatedResponseDTO<CompaniesDTO>> ListCompanies(
        CancellationToken cancellationToken = default(CancellationToken));
}