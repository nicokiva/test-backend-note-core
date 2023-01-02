using Faker;
using Microsoft.EntityFrameworkCore;
using Test.Repository;

namespace Test.API.Seeders;

public class DevelopmentSeeder : IApplicationSeeder
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DevelopmentSeeder> _logger;

    public DevelopmentSeeder(IUnitOfWork unitOfWork, ILogger<DevelopmentSeeder> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Seed()
    {
        try
        {
            var strategy = _unitOfWork.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    if (transaction == null)
                        throw new Exception("Begin db transaction async failed");
                    
                    if (! await _unitOfWork.CompanyRepository.CompanyExistsAsync("12345"))
                        await _unitOfWork.CompanyRepository.CreateAsync(Domain.Entities.Company.New(Faker.Company.Name(), "12345"));
                    if (! await _unitOfWork.CompanyRepository.CompanyExistsAsync("12346"))
                        await _unitOfWork.CompanyRepository.CreateAsync(Domain.Entities.Company.New(Faker.Company.Name(), "12346"));
                    if (! await _unitOfWork.CompanyRepository.CompanyExistsAsync("12347"))
                        await _unitOfWork.CompanyRepository.CreateAsync(Domain.Entities.Company.New(Faker.Company.Name(), "12347"));
                    await _unitOfWork.SaveEntitiesAsync();
                    await _unitOfWork.CommitTransactionAsync(transaction);
                    _logger.LogDebug("Development Seeder successfully completed!");
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
        }
    }
}