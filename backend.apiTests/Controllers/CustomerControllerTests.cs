using backend.api.Models.Generated;
using backend.api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.api.Controllers.Tests
{
    public class CustomerControllerTests
    {
        private readonly ICustomerService customer;
        public CustomerControllerTests()
        {
            customer = A.Fake<ICustomerService>();
        }

        [Fact()]
        public void GetAllCustomersTest()
        {
            Customer customerInfo = new()
            {
                Id = 1,
                Firstname = "Test",
                Middlename = "Test",
                Lastname = "Test",
                Age = 1,
                Sex = "M"
            };

            IList<Customer> customers = [
                    customerInfo
                ];

            A.CallTo(() => customer.GetAllCustomers()).Returns(customers);
            CustomersController sut = new(customer);
            IActionResult res = sut.GetAllCustomers();

            OkObjectResult? okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);

            IList<Customer>? returnedCustomers = okresult.Value as IList<Customer>;
            returnedCustomers.Should().NotBeNull();
            returnedCustomers.Should().HaveCount(1);
            Customer returnedCustomer = returnedCustomers![0];
            returnedCustomer.Should().BeEquivalentTo(customerInfo);
        }

        [Fact()]
        public void GetCustomerDetailsTest()
        {
            Customer customerInfo = new()
            {
                Id = 1,
                Firstname = "Test",
                Middlename = "Test",
                Lastname = "Test",
                Age = 1,
                Sex = "M"
            };

            A.CallTo(() => this.customer.GetCustomerDetails(A<int>.Ignored)).Returns(customerInfo);
            CustomersController sut = new(customer);
            IActionResult res = sut.GetCustomerDetails(1);

            OkObjectResult? okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);

            Customer? returnedCustomers = okresult.Value as Customer;
            returnedCustomers.Should().NotBeNull();
            returnedCustomers.Should().BeEquivalentTo(customerInfo);
        }

        [Fact()]
        public async Task AddCustomerTest()
        {
            Customer customerInfo = new()
            {
                Id = 1,
                Firstname = "Test",
                Middlename = "Test",
                Lastname = "Test",
                Age = 1,
                Sex = "M"
            };

            CustomersController sut = new(customer);
            IActionResult res = await sut.AddCustomer(customerInfo);

            ObjectResult? test = res as ObjectResult;
            test!.Should().NotBeNull();
        }

        [Fact()]
        public async Task EditCustomerTest()
        {
            Customer customerInfo = new()
            {
                Id = 1,
                Firstname = "Test",
                Middlename = "Test",
                Lastname = "Test",
                Age = 1,
                Sex = "M"
            };

            A.CallTo(() => this.customer.EditCustomer(A<Customer>.Ignored)).Returns(true);
            CustomersController sut = new(customer);
            IActionResult res = await sut.EditCustomer(customerInfo);

            NoContentResult? okresult = res as NoContentResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(204);
        }

        [Fact()]
        public async Task DeleteCustomerTest()
        {
            A.CallTo(() => this.customer.DeleteCustomer(A<int>.Ignored)).Returns(true);
            CustomersController sut = new(customer);
            IActionResult res = await sut.DeleteCustomer(1);

            ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(res);
            Assert.Equal(205, statusCodeResult.StatusCode);
        }
    }
}