using System.Net;
using MediatR;
using Microsoft.Extensions.Logging;
using Test.Application.Commands.Base;
using Test.Application.Validators.Base;

namespace Test.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : ICommandResponse, new() where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
        private readonly IBusinessValidationHandler<TRequest>? _businessValidationHandler;

        // Have 2 constructors incase the validator does not exist
        public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
        {
            this._logger = logger;
        }

        public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger, IBusinessValidationHandler<TRequest> businessValidationHandler)
        {
            this._logger = logger;
            this._businessValidationHandler = businessValidationHandler;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType();
            if (_businessValidationHandler == null)
            {
                _logger.LogInformation("{Request} does not have a validation handler configured", requestName);
                return await next();
            }

            var result = await _businessValidationHandler.Validate(request);
            if (!result.IsSuccessful)
            {
                _logger.LogWarning("Validation failed for {Request}. Error: {Error}", requestName, result.Error);
                return new TResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = result.Error,
                    FieldError = result.FieldError
                };
            }

            _logger.LogInformation("Validation successful for {Request}", requestName);
            return await next();
        }
    }

}