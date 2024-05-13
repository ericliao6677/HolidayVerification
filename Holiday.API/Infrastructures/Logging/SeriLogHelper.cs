using Serilog;
using Serilog.AspNetCore;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Holiday.API.Infrastructures.Logging
{
    public class SeriLogHelper
    {
        public static void ConfigureSerilLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.With<EventTypeEnricher>()
                .WriteTo.Console(new CompactJsonFormatter())
                .WriteTo.Map(// 寫入txt => 按照 level
                    evt => evt.Level,
                    (level, wt) => wt.File(
                        new CompactJsonFormatter(),
                        path: String.Format(configuration.GetValue<string>("Path:SerilLogSavePath"), level),
                        restrictedToMinimumLevel: LogEventLevel.Information,
                        rollOnFileSizeLimit: true,
                        rollingInterval: RollingInterval.Day))
                .CreateLogger();
        }

        public static string RequestPayload = "";

        public static async void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;
           
            diagnosticContext.Set("RequestBody", RequestPayload);

            string responseBodyPayload = await ReadResponseBody(httpContext.Response);
            diagnosticContext.Set("ResponseBody", responseBodyPayload);

            // Set all the common properties available for every request
            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);

            // Only set it if available. You're not sending sensitive data in a querystring right?!
            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }

            // Set the content-type of the Response at this point
            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

            // Retrieve the IEndpointFeature selected for the request
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is object) // endpoint != null
            {
                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
            }
        }

        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{responseBody}";
        }
    }
}
