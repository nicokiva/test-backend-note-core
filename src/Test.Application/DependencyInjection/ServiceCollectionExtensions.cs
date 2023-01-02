using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Test.Application.DTOs;
using Test.Application.DTOs.Base;
using Test.Application.Queries.Base;
using Test.Application.Validators.Base;

namespace Test.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    
    public static IServiceCollection AddApplication(this IServiceCollection services, string connectionString)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IBusinessValidationHandler>()
            .AddClasses(classes => classes.AssignableTo<IBusinessValidationHandler>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviours.LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviours.ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviours.TransactionBehaviour<,>));

        services.AddAutoMapper(typeof(IDTO).Assembly);
        
        services.Scan(scan => scan
            .FromAssemblyOf<IQueriesCollection>()
            .AddClasses(classes => classes.AssignableTo<IQueriesCollection>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        
        services.AddSingleton<QueryConnectionString>((_) =>
        {
            return new QueryConnectionString(connectionString);
        });
        
        return services;
    }

}