using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Test.Repository;

public interface IUnitOfWork 
{
    /// <summary>
    /// Add your repository below
    /// </summary>
    ICompanyRepository CompanyRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    
    DatabaseFacade Database { get; }
    IDbContextTransaction? GetCurrentTransaction();
    bool HasActiveTransaction { get; }
    Task<IDbContextTransaction?> BeginTransactionAsync();
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    void RollbackTransaction();
}