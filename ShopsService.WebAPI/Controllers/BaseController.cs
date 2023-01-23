using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopsService.Common;
using System.Diagnostics.CodeAnalysis;

namespace ShopsService.WebAPI.Controllers
{
    [ApiController]
    [ExcludeFromCodeCoverage]
    public abstract class BaseController<TController> : ControllerBase
    {
         private readonly ILogger<TController> _logger;

        protected BaseController(
            ILogger<TController> logger) => _logger = logger;
         
         
        protected ObjectResult ReturnCommonResult(dynamic value)
        {
            if (value == null)
                return new BadRequestObjectResult("");

            if (value.IsError)
                return new BadRequestObjectResult(value);
            else
            {
                var json = JsonConvert.SerializeObject(value);
                return new OkObjectResult(json);
            }
        }
         

        [NonAction]
        public override BadRequestObjectResult BadRequest(object error)
        {
            var json = JsonConvert.SerializeObject(error);
            return new BadRequestObjectResult(json);
        }
         
       
         
    }
}
