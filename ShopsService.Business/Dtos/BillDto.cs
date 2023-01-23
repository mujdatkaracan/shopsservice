using ShopsService.Business.Dtos.Base;
using System.ComponentModel.DataAnnotations;

namespace ShopsService.Business.Dtos
{
    public class BillDto : BaseDto
    {

        [Required]
        public List<Item> Items { get; set; } = null!;
    }
}
