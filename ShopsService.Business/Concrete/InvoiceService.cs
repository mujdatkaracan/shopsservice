using ShopsService.Business.Abstract;
using ShopsService.Business.Constants;
using ShopsService.Business.Dtos;
using ShopsService.Common;
using ShopsService.Domain.Enums;

namespace ShopsService.Business.Concrete
{
    public class InvoiceService : IInvoiceService
    {
       


        public async Task<CommonResult<string>> GetInvoiceAmount(BillDto bill, CustomerDto user)
        {
            CommonResult<string> result;
            try
            {  
                if (user != null)
                {
                    var billAmount = GetTotalAmount(bill.Items); 
                    decimal affiliateDiscount = user.Type == CustomerType.Affiliate ? GetDiscount(bill.Items, user) : 0;
                    decimal employeeDiscount = user.Type ==CustomerType.Employee ? GetDiscount(bill.Items, user) : 0; 
                    decimal customerDiscount = user.Type == CustomerType.Other ? GetDiscount(bill.Items, user) : 0;

                    var fiveDiscountCount = (int)billAmount / 100;
                    var fiveDiscount = (fiveDiscountCount < 1) ? 0 : DiscountMatrix.FiveDollarDiscount * fiveDiscountCount;

                    var totalDiscount = affiliateDiscount + employeeDiscount + customerDiscount + fiveDiscount;

                    var billAmountAfterDiscount = billAmount - totalDiscount;
 
                    result = CommonResult<string>.CreateResult(billAmountAfterDiscount.ToString());
                }
                else
                {
                    result = CommonResult<string>.CreateResult(null);
                }
            }
            catch (Exception ex)
            {
                result = CommonResult<string>.CreateError(System.Net.HttpStatusCode.BadRequest,ex.Message);

            }

            return result;
        }

        #region Helper Methods
        private decimal GetDiscount(List<Item> items,CustomerDto customer)
        { 
            var billAmountLessGroceries = GetTotalAmountExcludingGroceries(items);

            decimal discount;
            switch (customer.Type)
            {
                case CustomerType.Employee:
                    discount = DiscountMatrix.EmployeeDiscount;
                    break;
                case CustomerType.Affiliate:
                    discount = DiscountMatrix.AffiliateDiscount;
                    break;
                case CustomerType.Other: 
                    discount = DateTime.Now > (customer.ActivationDate.AddYears(2)) ? DiscountMatrix.CustomersDiscount : 0;
                    break;
                default:
                    discount = 0;
                    break;
            }
            return discount * billAmountLessGroceries;

        }

        public decimal GetTotalAmount(List<Item> items)
        {
            var itemAmounts = items.Select(x => x.Price).ToList();
            var billAmount = itemAmounts.Sum();
            return billAmount;
        }
         
        public decimal GetTotalAmountExcludingGroceries(List<Item> items)
        {
            var itemsExcludingGroceries = items.Where(x => x.Type !=  ProductType.Groceries).ToList();
            var itemAmounts = itemsExcludingGroceries.Select(x => x.Price).ToList();
            var billAmount = itemAmounts.Sum();
            return billAmount;
        }

        #endregion
    }
}
