using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopsService.Common;
using System.Net;

namespace ShopsService.WebAPI.Extensions
{
    /// <summary>
    /// Extension class for API behavior
    /// </summary>
    public static class ApiBehaviorExtensions
    {
        /// <summary>
        /// Register api behavior
        /// </summary> 
        public static void AddApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    var message = string.Join("\r\n", actionContext.ModelState.Values.SelectMany(x => x.Errors)
                        .Select(x => string.IsNullOrEmpty(x.ErrorMessage) ? x.Exception?.ToString() : x.ErrorMessage));

                    var result = CommonResult<object>.CreateError(HttpStatusCode.BadRequest, message); 
                    var json = JsonConvert.SerializeObject(result);
                    return new BadRequestObjectResult(json);
                };
            });
        }
    }
}
