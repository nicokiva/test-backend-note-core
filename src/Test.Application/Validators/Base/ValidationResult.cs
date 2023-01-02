namespace Test.Application.Validators.Base
{
    public class ValidationResult
    {
        public bool IsSuccessful { get; set; } = true;
        public string? Error { get; init; }
        public FieldError? FieldError { get; init; }

        public static ValidationResult Success => new ValidationResult();
        public static ValidationResult Fail(string error) => new ValidationResult { IsSuccessful = false, Error = error};
        public static ValidationResult Fail(FieldError error) => new ValidationResult { IsSuccessful = false, Error = error.ErrorMessage, FieldError = error};
    }
}