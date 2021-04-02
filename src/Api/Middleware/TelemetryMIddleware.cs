using System;
using System.Threading.Tasks;
using Api.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Api.Middleware
{
    public class TelemetryMiddleware
    {
        private RequestDelegate _next;

        public TelemetryMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<TelemetryAttribute>();
            if (attribute != null)
            {
               Console.WriteLine($"Telemetry logging call { endpoint.DisplayName}");
            }
        }
    }
}