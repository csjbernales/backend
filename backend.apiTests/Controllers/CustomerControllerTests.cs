using backend.api.Models.Generated;
using Xunit;

namespace backend.api.Controllers.Tests
{
    public class CustomerControllerTests
    {
        private readonly ICustomer customer;

        public CustomerControllerTests()
        {
            customer = A.Fake<ICustomer>();
        }

        [Fact()]
        public void GetAllCustomersTest()
        {
            Assert.True(true);
        }

        [Fact()]
        public void GetCustomerDetailsTest()
        {
            Assert.True(true);
        }

        [Fact()]
        public void AddCustomerTest()
        {
            Assert.True(true);
        }

        [Fact()]
        public void EditCustomerTest()
        {
            Assert.True(true);
        }

        [Fact()]
        public void DeleteCustomerTest()
        {
            Assert.True(true);
        }
    }
}