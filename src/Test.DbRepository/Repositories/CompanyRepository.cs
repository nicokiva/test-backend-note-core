using Microsoft.EntityFrameworkCore;
using Test.DbRepository.Repositories.Base;
using Test.Domain.Entities;
using Test.Repository;

namespace Test.DbRepository.Repositories;

public class CompanyRepository: EntityFrameworkRepository<Company>, ICompanyRepository
{
    public CompanyRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> CompanyExistsAsync(string companyCode)
    {
        return await DbContext.Set<Company>().AnyAsync(c => c.Code == companyCode);
    }
}