using ShopsService.Business.Dtos;
using ShopsService.Common;

namespace ShopsService.Business.Abstract
{
    public interface IInvoiceService
    {
        Task<CommonResult<string>> GetInvoiceAmount(BillDto bill,CustomerDto user);
    }
}
