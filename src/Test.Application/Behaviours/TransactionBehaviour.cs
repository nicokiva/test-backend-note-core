using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Test.Application.Commands.Base;
using Test.Domain.Extensions;
using Test.Repository;

namespace Test.Application.Behaviours;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : ICommandResponse, new() where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;

    public TransactionBehaviour(
        IUnitOfWork unitOfWork,
        ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
    {
        _unitOfWork= unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentException(nameof(ILogger));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();

        try
        {
            if (_unitOfWork.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = _unitOfWork.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {

                using (var transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    if (transaction == null)
                        throw new Exception("Unexpected condition. transaction null when not expected");
                    
                    using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})",
                            transaction.TransactionId, typeName, request);

                        response = await next();

                        _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}",
                            transaction.TransactionId, typeName);

                        await _unitOfWork.CommitTransactionAsync(transaction);

                    }
                }
            });

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}
