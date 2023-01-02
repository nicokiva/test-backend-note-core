using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Commands.Base;
using Test.Domain.Extensions;

namespace Test.Application.Behaviours;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ICommandResponse, new() where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
        var response = await next();
        _logger.LogInformation("----- Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);

        return response;
    }
}
