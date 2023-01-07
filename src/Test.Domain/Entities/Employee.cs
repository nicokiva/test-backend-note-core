using Test.Domain.Seed;

namespace Test.Domain.Entities;

public class Employee : Entity
{

    public static Employee New(
    string fullName,
    string idNumber,
    DateTime dateOfBirth,
    Guid company)
    {
        var employee = new Employee(
            Guid.Empty,
            fullName,
            idNumber,
            dateOfBirth,
            company,
            null,
            null,
            DateTime.UtcNow,
            DateTime.UtcNow
            );

        return employee;
    }

    private Employee(
        Guid id,
        string fullName,
        string idNumber,
        DateTime dateOfBirth,
        Guid company,
        string? createdBy,
        string? updatedBy,
        DateTime createdAt,
        DateTime updatedAt) : base(id, createdBy, updatedBy, createdAt, updatedAt)
    {
        FullName = fullName;
        IdNumber = idNumber;
        DateOfBirth = dateOfBirth;
        Company = company;
    }

    public string FullName { get; private set; }
    public string IdNumber { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Guid Company { get; private set; }

}

