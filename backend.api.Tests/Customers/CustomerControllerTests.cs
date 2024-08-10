using backend.api.Customers;
using backend.api.Customers.Interface;
using backend.data.Models.Dto;
using backend.data.Models.Generated;
using Microsoft.AspNetCore.Mvc;

namespace backend.api.Tests.Customers
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
            CustomersDto customerInfo = new(Guid.NewGuid(), "test", "test", "test", 1, "m", true);

            IReadOnlyList<CustomersDto> customers = [
                    customerInfo
                ];

            A.CallTo(() => customer.GetAllCustomers()).Returns(customers);
            CustomersController sut = new(customer);
            IActionResult res = sut.GetAllCustomers();

            OkObjectResult? okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);

            IList<CustomersDto>? returnedCustomers = okresult.Value as IList<CustomersDto>;
            returnedCustomers.Should().NotBeNull();
            returnedCustomers.Should().HaveCount(1);
            CustomersDto returnedCustomer = returnedCustomers![0];
            returnedCustomer.Should().BeEquivalentTo(customerInfo);
        }

        [Fact()]
        public void GetCustomerDetailsTest()
        {
            Guid guid = Guid.NewGuid();
            var customer = new(guid, "test", "test", "test", 1, "m", true);
            List<CustomersDto> customersDtos = new List<CustomersDto>();
            customersDtos.Add(customer);

            A.CallTo(() => customer.GetCustomerDetails(A<List<Guid>>.Ignored)).Returns(customerInfo);
            CustomersController sut = new(customer);
            IActionResult res = sut.GetCustomerDetails(guid);

            OkObjectResult? okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);

            CustomersDto? returnedCustomers = okresult.Value as CustomersDto;
            returnedCustomers.Should().NotBeNull();
            returnedCustomers.Should().BeEquivalentTo(customerInfo);
        }

        [Fact()]
        public async Task AddCustomerTest()
        {
            Customer customerInfo = new()
            {
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
                Firstname = "Test",
                Middlename = "Test",
                Lastname = "Test",
                Age = 1,
                Sex = "M"
            };

            A.CallTo(() => customer.EditCustomer(A<Customer>.Ignored)).Returns(true);
            CustomersController sut = new(customer);
            IActionResult res = await sut.EditCustomer(customerInfo);

            NoContentResult? okresult = res as NoContentResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(204);
        }

        [Fact()]
        public async Task DeleteCustomerTest()
        {
            A.CallTo(() => customer.DeleteCustomer(A<Guid>.Ignored)).Returns(true);
            CustomersController sut = new(customer);
            IActionResult res = await sut.DeleteCustomer(Guid.NewGuid());

            ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(res);
            Assert.Equal(205, statusCodeResult.StatusCode);
        }
    }
}