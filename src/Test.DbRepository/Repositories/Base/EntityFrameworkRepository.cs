using Microsoft.EntityFrameworkCore;
using Test.Domain.Seed;
using Test.Repository;

namespace Test.DbRepository.Repositories.Base;

public class EntityFrameworkRepository<T> : IRepository<T> where T : Entity
{
    protected readonly DbContext DbContext;
    private readonly string? _currentUser = null;

    protected EntityFrameworkRepository(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<T?> ReadAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await DbContext.Set<T>().FindAsync(id, cancellationToken);
    }

    public virtual T? Read(Guid id)
    {
        return DbContext.Set<T>().Find(id);
    }

    public virtual async Task<T?> FindAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await DbContext.FindAsync<T>(new object[] { id }, cancellationToken);
    }

    public virtual T? Find(Guid id)
    {
        return DbContext.Find<T>(id);
    }


    public virtual T Create(T entity)
    {
        entity.CreatedBy = _currentUser!;
        return DbContext.Set<T>().Add(entity).Entity;
    }

    public virtual void Delete(T entity)
    {
        DbContext.Set<T>().Remove(entity);
    }

    public virtual void Update(T entity)
    {
        entity.UpdatedBy = _currentUser!;
        DbContext.Set<T>().Update(entity);
    }

    public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        entity.CreatedBy = _currentUser!;
        return (await DbContext.Set<T>().AddAsync(entity, cancellationToken)).Entity;
    }
}