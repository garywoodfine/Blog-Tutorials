using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Boleyn.Countries.Activities.Country.Get;
using Boleyn.Countries.Resources;
using FluentValidation;
using MediatR;
using Serilog;
using Threenine.ApiResponse;
using ValidationException = Boleyn.Countries.Content.Exceptions.ValidationException;

namespace Boleyn.Countries.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger _logger;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {

            if (!typeof(TResponse).IsGenericType) return await next();
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults =
                await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors)
                .Where(f => f != null)
                .GroupBy(   x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            if (!failures.Any()) return await next();
            
            var responseType = typeof(TResponse).GetGenericArguments()[0];
            var invalidResponseType = typeof(SingleResponse<>).MakeGenericType(responseType);
            var inValidResponse = Activator.CreateInstance(invalidResponseType, null, failures.ToList()) as TResponse;
            return inValidResponse;

        }
    }
}