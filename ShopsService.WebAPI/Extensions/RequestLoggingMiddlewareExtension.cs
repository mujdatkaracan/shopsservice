using ShopsService.WebAPI.Middlewares;

namespace ShopsService.WebAPI.Extensions
{
    public static class RequestLoggingMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
