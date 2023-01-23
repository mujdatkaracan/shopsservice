using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;

namespace ShopsService.WebAPI.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HealthCheckExtension
    {
        public static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
         services.AddHealthChecks();
             
            services.AddHealthChecksUI(setupSettings =>
            {
                setupSettings.SetEvaluationTimeInSeconds(30);
                setupSettings.MaximumHistoryEntriesPerEndpoint(30);
                setupSettings.SetApiMaxActiveRequests(1);
                setupSettings.AddHealthCheckEndpoint("ShopsRUs Service Health Status List",
                    "/health-api");
            }).AddInMemoryStorage();
        }

        public static void UseHealthCheck(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health-api",
            new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
             
            app.UseHealthChecksUI(delegate (Options options) { options.UIPath = "/health-ui"; });
        }
    }
}
