using System.Text.Json.Serialization;
using MediatR;

namespace Test.Domain.Seed;

public abstract class Entity: IEntity
{
    int? _requestedHashCode;
    Guid _id;
    
    public Entity(
        Guid id,
        string? createdBy,
        string? updatedBy,
        DateTime createdAt,
        DateTime updatedAt)
    {
        _id = id;
        _createdBy = createdBy;
        _updatedBy = updatedBy;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
    }

    public Entity()
    {
    }

    public virtual Guid Id
    {
        get { return _id; }
        protected set { _id = value; }
    }


    private string? _updatedBy;
    private string? _createdBy;

    private DateTime _createdAt;

    public DateTime CreatedAt
    {
        get => _createdAt;
        set => _createdAt = value;
    }

    private DateTime _updatedAt;

    public DateTime UpdatedAt
    {
        get => _updatedAt;
        set => _updatedAt = value;
    }

    public string UpdatedBy
    {
        get => _updatedBy!;
        set => _updatedBy = value;
    }

    public string CreatedBy
    {
        get => _createdBy!;
        set => _createdBy = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Entity))
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        Entity item = (Entity) obj;

        return item.Id.Equals(this.Id);
    }

    public override int GetHashCode()
    {
        if (!_requestedHashCode.HasValue)
            _requestedHashCode =
                this.Id.GetHashCode() ^
                31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

        return _requestedHashCode.Value;
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null)) ? true : false;
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}