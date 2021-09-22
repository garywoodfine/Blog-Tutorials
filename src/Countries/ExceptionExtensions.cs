using System;
using System.Collections.Generic;
using System.Linq;

namespace Boleyn.Countries.Content
{
    public static class ExceptionExtensions
    {
        public static List<Exception> FlattenInnerExceptions(this Exception exception)
        {
            return exception
                .DepthFirstSearchInnerExceptions()
                .Distinct()
                .Reverse()
                .ToList();
        }

        private static IEnumerable<Exception> DepthFirstSearchInnerExceptions(this Exception exception)
        {
            if (exception is not AggregateException) yield return exception;
            var bypassInnerException = exception;

            do
            {
                switch (bypassInnerException)
                {
                    case AggregateException { InnerExceptions: { } innerExceptions } aggregateException when innerExceptions.Any():
                        foreach (var innerException in aggregateException.FlattenAggregateExceptions())
                        {
                            yield return innerException;
                        }
                        break;

                    case { InnerException: AggregateException { InnerExceptions: { } innerExceptions } aggregateException } when innerExceptions.Any():
                        foreach (var innerException in aggregateException.FlattenAggregateExceptions())
                        {
                            yield return innerException;
                        }
                        break;

                    case { InnerException: { } innerException }:
                        yield return innerException;
                        break;

                    default:
                        yield return bypassInnerException;
                        break;
                }

                bypassInnerException = bypassInnerException.InnerException;
            } while (bypassInnerException is not null);
        }

        private static IEnumerable<Exception> FlattenAggregateExceptions(this AggregateException aggregateException)
        {
            return aggregateException
                .Flatten().InnerExceptions
                .SelectMany(DepthFirstSearchInnerExceptions);
        }
    }
}