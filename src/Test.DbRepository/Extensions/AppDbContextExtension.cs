using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Test.Repository;

namespace Test.DbRepository.Extensions;

public static class AppDbContextExtension
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, string connectionString, Action<NpgsqlDbContextOptionsBuilder> npgsqlOptions)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<UnitOfWork>()
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>))) 
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions).UseSnakeCaseNamingConvention();
        }, ServiceLifetime.Scoped);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
    
    public static IHost MigrateDbContext<TContext>(this IHost webHost, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {
        using (var scope = webHost.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<TContext>();
            context.Database.Migrate();
            seeder(context, services);
        }

        return webHost;
    }

}