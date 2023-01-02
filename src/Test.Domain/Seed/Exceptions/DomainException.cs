namespace Test.Domain.Seed.Exceptions;

public class DomainException : ApplicationException
{
    public DomainException(string message) : base(message) { }
}