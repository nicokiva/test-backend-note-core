using System.Linq.Expressions;
using Test.Domain.Seed;

namespace Test.Repository;

public interface IGenericRepository {}

public interface IRepository<T>:IGenericRepository where T: Entity
{
    Task<T?> ReadAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    T? Read(Guid id);
    Task<T?> FindAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    T? Find(Guid id);
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
    T Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}