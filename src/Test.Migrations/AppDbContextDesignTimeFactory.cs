using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Test.DbRepository;

namespace Test.Migrations;

public class AppDbContextDesignTimeFactory: IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql(".", options =>
        {
            options.MigrationsAssembly(GetType().Assembly.GetName().Name);
        }).UseSnakeCaseNamingConvention();

        return new AppDbContext(optionsBuilder.Options);
    }
}