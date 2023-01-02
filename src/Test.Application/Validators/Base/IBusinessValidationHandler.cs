namespace Test.Application.Validators.Base
{
    public interface IBusinessValidationHandler { }

    public interface IBusinessValidationHandler<T> : IBusinessValidationHandler
    {
        Task<ValidationResult> Validate(T request);
    }
}