using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopsService.Business.Abstract;
using ShopsService.Business.Concrete;
using ShopsService.Common;

namespace ShopsService.Business
{
    public static class DependencyInjection
    {
        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddScoped<IInvoiceService, InvoiceService>();
        }

    }
}
