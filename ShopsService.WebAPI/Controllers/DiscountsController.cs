using Microsoft.AspNetCore.Mvc;
using ShopsService.Business.Abstract;
using ShopsService.Business.Concrete;
using ShopsService.Business.Dtos;

namespace ShopsService.WebAPI.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class DiscountsController : BaseController<DiscountsController>
    {
        private readonly IInvoiceService _invoiceService;

        public DiscountsController(IInvoiceService invoiceService,
            ILogger<DiscountsController> logger) : base(logger)
        {
            _invoiceService = invoiceService;
        }
         
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] InvoiceRequestDto invoiceRequestModel)
        { 
            return ReturnCommonResult(await _invoiceService.GetInvoiceAmount(invoiceRequestModel.Bill, invoiceRequestModel.User));
             
        }
    }
}
