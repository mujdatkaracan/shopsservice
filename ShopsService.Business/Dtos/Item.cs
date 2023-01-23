using ShopsService.Business.Dtos.Base;
using ShopsService.Domain.Enums;

namespace ShopsService.Business.Dtos
{
    public class Item:BaseDto
    { 
        public ProductType Type { get; set; }

        public decimal Price { get; set; }
    }
}
