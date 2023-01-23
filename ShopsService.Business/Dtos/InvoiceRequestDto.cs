using ShopsService.Business.Dtos.Base;
using System.ComponentModel.DataAnnotations;

namespace ShopsService.Business.Dtos
{
    public class InvoiceRequestDto : BaseDto
    {
        [Required]
        public CustomerDto User { get; set; }

        [Required]
        public BillDto Bill { get; set; }
    }
}
