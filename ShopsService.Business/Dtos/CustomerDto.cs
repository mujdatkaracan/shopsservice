using ShopsService.Business.Dtos.Base;
using ShopsService.Domain.Enums;

namespace ShopsService.Business.Dtos
{
    public class CustomerDto:BaseDto
    {
        public DateTime ActivationDate { get; set; }
        public CustomerType Type { get; set; }
    }
}
