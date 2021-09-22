using System;
using System.Threading;
using System.Threading.Tasks;
using Boleyn.Countries.Content;
using MediatR;
using Serilog;
using Serilog.Events;

namespace Boleyn.Countries.Behaviours
{
    public class ExceptionsHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next().ConfigureAwait(false);
            }
            catch (InvalidOperationException exception)
            {
                var exceptions = exception.FlattenInnerExceptions();

                foreach (var ex in exceptions)
                {
                    Log.Write(LogEventLevel.Warning, string.Concat("{Exception}: ", ex.Message), ex.GetType().FullName);
                }

                throw new AggregateException("One or more errors occurred", exceptions);
            }
        }
    }
}