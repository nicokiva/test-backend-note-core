using Microsoft.EntityFrameworkCore;
using Test.DbRepository.Repositories.Base;
using Test.Domain.Entities;
using Test.Repository;

namespace Test.DbRepository.Repositories
{
    public class EmployeeRepository : EntityFrameworkRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> EmployeeExistsByIdNumber(string idNumber)
        {
            return await DbContext.Set<Employee>().AnyAsync(c => c.IdNumber == idNumber);
        }
    }
}

