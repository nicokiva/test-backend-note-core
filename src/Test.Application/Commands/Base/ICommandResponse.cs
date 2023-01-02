using System.Net;
using Test.Application.Validators.Base;

namespace Test.Application.Commands.Base
{
    public interface ICommandResponse
    {
        public HttpStatusCode StatusCode { get; init; }
        public string? ErrorMessage { get; init; }
        public FieldError? FieldError { get; init; }
        public object? Data { get; }
    }
}