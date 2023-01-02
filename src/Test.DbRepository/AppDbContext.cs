using Microsoft.EntityFrameworkCore;

namespace Test.DbRepository;

public sealed class AppDbContext: DbContext
{
    public AppDbContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}