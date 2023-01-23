using ShopsService.Business.Concrete;
using ShopsService.Business.Dtos;
using ShopsService.Domain.Enums;

namespace ShopsService.Test
{
    public class ServiceBase
    {
        [Fact]
        public async Task StandardUserOlderThan2YearsGetsPercentageDiscount()
        {
            #region Arrange 

            var bill = new BillDto
            {
                Items = new List<Item> {
                    new Item
                {
                    Price = 50,
                    Type = ProductType.Other
                }}
            };
            var user = new CustomerDto
            {
                Type = CustomerType.Other,
                ActivationDate = DateTime.Now.AddYears(-3),
            };
            #endregion

            var invoiceService = GetInvoiceService();
            //act
            var result = await invoiceService.GetInvoiceAmount(bill, user);

            //assert

            Assert.Equal(200, result.StatusCode);
            Assert.False(result.IsError);
            Assert.Equal("47.50", result.Data.ToString());
        }

        [Fact]
        public async Task NewStandardUserGetsNoPercentageDiscount()
        {
            #region Arrange
            var bill = new BillDto
            {
                Items = new List<Item> {
                    new Item
                {
                    Price = 100,
                    Type = ProductType.Other
                }}
            };
            var user = new CustomerDto
            {
                Type = CustomerType.Other,
                ActivationDate = DateTime.Now,
            };
            #endregion

            var invoiceService = GetInvoiceService();
            //act
            var result = await invoiceService.GetInvoiceAmount(bill, user);

            //assert
            Assert.Equal("95", result.Data.ToString());
        }

        [Fact]
        public async Task AffiliateUserGetsAffiliateAndFiveDiscount()
        {
            #region Arrange

            var bill = new BillDto
            {
                Items = new List<Item> {
                    new Item
                {
                    Price = 100,
                    Type = ProductType.Other
                }}
            };
            var user = new CustomerDto
            {
                Type = CustomerType.Affiliate,
                ActivationDate = DateTime.Now,
            };
            #endregion

            var invoiceService = GetInvoiceService();
            //act
            var result = await invoiceService.GetInvoiceAmount(bill, user);

            //assert
            Assert.Equal("85.0", result.Data.ToString());
        }

        [Fact]
        public async Task EmployeeUserGetsEmployeeAndFiveDiscount()
        {

            #region Arrange
            var bill = new BillDto
            {
                Items = new List<Item> {
                    new Item
                {
                    Price = 100,
                    Type = ProductType.Other
                }}
            };
            var user = new CustomerDto
            {
                Type = CustomerType.Employee,
                ActivationDate = DateTime.Now,
            };
            #endregion

            var invoiceService = GetInvoiceService();
            //act
            var result = await invoiceService.GetInvoiceAmount(bill, user);

            //assert
            Assert.Equal("65.0", result.Data.ToString());
        }

        [Fact]
        public async Task NoPercentageDiscountOnGroceries()
        {
            #region Arrange
            var bill = new BillDto
            {
                Items = new List<Item> {
                    new Item
                {
                    Price = 90,
                    Type = ProductType.Groceries
                }}
            };
            var user = new CustomerDto
            {
                Type = CustomerType.Affiliate,
                ActivationDate = DateTime.Now,
            };
            #endregion
            var invoiceService = GetInvoiceService();

            //act
            var result = await invoiceService.GetInvoiceAmount(bill, user);

            var value = result.Data?.ToString();
            //assert
            Assert.False(result.IsError);
            Assert.Equal("90.0", value);
        }

        [Fact]
        public void GetTotalAmountTest()
        {
            #region Arrange 
            var items = new List<Item> {
                    new Item
                {
                    Price = 50,
                    Type = ProductType.Groceries
                }, new Item
                {
                    Price = 100,
                    Type = ProductType.Groceries,
                }, new Item
                {
                    Price = 100,
                    Type =ProductType.Other
                }, new Item
                {
                    Price = 100,
                    Type =ProductType.Other
                },
                };

            #endregion

            var invoiceService = GetInvoiceService();

            //act
            var result = invoiceService.GetTotalAmount(items);

            //assert
            Assert.Equal(350.0m, result);

        }

        [Fact]
        public void GetTotalAmountExcludingGroceriesTest()
        {
            #region Arrange
            var items = new List<Item>
            {
                new Item
                {
                    Price = 50,
                    Type = ProductType.Groceries
                },
                new Item
                {
                    Price   = 100,
                    Type = ProductType.Groceries
                },
                 new Item
                {
                    Price = 100,
                    Type = ProductType.Other
                },
                 new Item
                {
                    Price = 100,
                    Type = ProductType.Other,
                }
            };
            #endregion

            var invoiceService = GetInvoiceService();

            //act
            var result = invoiceService.GetTotalAmountExcludingGroceries(items);

            //assert
            Assert.Equal(200.0m, result);

        }

        private InvoiceService GetInvoiceService()
        {
            var invoiceService = new InvoiceService();
            return invoiceService;
        }
    }
}