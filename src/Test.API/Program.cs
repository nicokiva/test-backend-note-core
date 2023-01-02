using FluentValidation.AspNetCore;
using Serilog;
using Test.API.Filters;
using Test.API.Seeders;
using Test.Application.DependencyInjection;
using Test.DbRepository;
using Test.DbRepository.Extensions;
using Test.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

// Application DbContext
builder.Services.AddAppDbContext(builder.Configuration.GetConnectionString("AppConnectionString"), options =>
{
    options.MigrationsAssembly(typeof(AppDbContextDesignTimeFactory).Assembly.GetName().Name);
});

// Application dependencies (Queries, Pipelines...)
builder.Services.AddApplication(builder.Configuration.GetConnectionString("AppConnectionString"));


builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ResponseMappingFilter));
    options.Filters.Add(typeof(BadRequestResponseMappingFilter));
    options.Filters.Add(typeof(ExceptionFilter));
}).AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Development Seeder
builder.Services.AddScoped<IApplicationSeeder, DevelopmentSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MigrateDbContext<AppDbContext>((_, sp) =>
    {
        var seeder = sp.GetService<IApplicationSeeder>();
        if (seeder != null)
        {
            seeder.Seed().Wait();
        }
    });
}
app.Run();