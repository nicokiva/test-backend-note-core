using Test.Domain.Seed;

namespace Test.Domain.Entities;

public class Company: Entity
{
    
    public static Company New(
        string name,
        string code
        )
    {
        var organization = new Company(
            Guid.Empty ,
            name,
            code,
            true,
            null,
            null,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
        
        return organization;
    }
    
    private Company(
        Guid id,
        string name,
        string code,
        bool active,
        string? createdBy,
        string? updatedBy,
        DateTime createdAt,
        DateTime updatedAt): base(id, createdBy, updatedBy, createdAt, updatedAt) 
    {
        Name = name;
        Code = code;
        Active = active;
    }

    public string Name { get; private set; }
    public string Code { get; private set; }
    public bool Active { get; private set; }
    
}