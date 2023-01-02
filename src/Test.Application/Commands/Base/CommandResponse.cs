using System.Net;
using Test.Application.Validators.Base;

namespace Test.Application.Commands.Base
{
    public record CommandResponse<T> : ICommandResponse where T : ICommandDataResponse, new()
    {
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
        public string? ErrorMessage { get; init; }
        public FieldError? FieldError { get; init; }
        public object? Data { get => Result; }
        public T? Result { get; init; }
    }
}