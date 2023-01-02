using Test.Domain.Entities;

namespace Test.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<bool> EmployeeExistsByIdNumber(string idNumber);
    }
}

