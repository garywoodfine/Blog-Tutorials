using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Boleyn.Countries.Content.Exceptions;
using FluentValidation;
using MediatR;
using Serilog;

namespace Boleyn.Countries.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
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
            if (!_validators.Any()) return await next();


            var context = new ValidationContext<TRequest>(request);
            var validationResults =
                await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            _logger.Information(failures.ToString());
            if (!failures.Any()) return await next();

            var sb = new StringBuilder();
            failures.ForEach(f =>
            {
                _logger.Information($"Validation error {f} ");
                sb.Append(f.ErrorMessage);
            });
            throw new CountryValidationException(sb.ToString());
        }
    }
}