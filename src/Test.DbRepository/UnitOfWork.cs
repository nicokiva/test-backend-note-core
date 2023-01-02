using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Test.Repository;

namespace Test.DbRepository;

public class UnitOfWork: IUnitOfWork
{
    private IDbContextTransaction? _currentTransaction = null;
    private readonly AppDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly ICompanyRepository _companyRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public UnitOfWork(
        AppDbContext dbContext,
        IMediator mediator,
        ICompanyRepository companyRepository,
        IEmployeeRepository employeeRepository)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _companyRepository = companyRepository;
        _employeeRepository = employeeRepository;
    }

    public ICompanyRepository CompanyRepository => _companyRepository;

    public IEmployeeRepository EmployeeRepository => _employeeRepository;

    public DatabaseFacade Database => _dbContext.Database;
    
    public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;
    
    public async Task<IDbContextTransaction?> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        //await _mediator.DispatchDomainEvents(_dbContext);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}