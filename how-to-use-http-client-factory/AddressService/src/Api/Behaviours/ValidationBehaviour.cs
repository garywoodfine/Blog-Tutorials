using FluentValidation;
using MediatR;
using Threenine.ApiResponse;
using ILogger = Serilog.ILogger;

namespace Api.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest,TResponse>
        where TRequest : IRequest<TResponse> where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger _logger;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger logger)
        {
            _validators = validators;
            _logger = logger;
        }
        

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!typeof(TResponse).IsGenericType) return await next();
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults =
                await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors)
                .Where(f => f != null)
                .GroupBy(x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            if (!failures.Any()) return await next();
            _logger.Error("Validation Errors",failures);
            return Activator.CreateInstance(typeof(TResponse), null, failures.ToList()) as TResponse;
        }
    }
}