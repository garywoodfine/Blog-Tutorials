using System.Threading.Tasks;
using Cms.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Serilog;

namespace Cms.Middleware
{
    public class TelemetryMiddleware
    {
        private RequestDelegate _next;
        private static readonly ILogger Log = Serilog.Log.ForContext<TelemetryAttribute>();
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
              
                Log.Information($"Telemetry logging call { endpoint}");
            }
        }
    }
}