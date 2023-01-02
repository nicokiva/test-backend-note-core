using Test.Domain.Entities;

namespace Test.Repository;

public interface ICompanyRepository : IRepository<Company>
{
    //
    // Add specific repository methods here.
    //
    Task<bool> CompanyExistsAsync(string companyCode);
}