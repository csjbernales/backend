using backend.api.Data.Generated;
using backend.api.Models.Generated;
using backend.api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var customerInfo = new Customer
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
            var sut = new CustomerController(customer);
            var res = sut.GetAllCustomers();

            var okresult = res as OkObjectResult;
            
            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);

            var returnedCustomers = okresult.Value as IList<Customer>;
            returnedCustomers.Should().NotBeNull();
            returnedCustomers.Should().HaveCount(1);
            var returnedCustomer = returnedCustomers![0];
            returnedCustomer.Should().BeEquivalentTo(customerInfo);
        }

        [Fact()]
        public void GetCustomerDetailsTest()
        {
            var customerInfo = new Customer
            {
                Id = 1,
                Firstname = "Test",
                Middlename = "Test",
                Lastname = "Test",
                Age = 1,
                Sex = "M"
            };

            A.CallTo(() => this.customer.GetCustomerDetails(A<int>.Ignored)).Returns(customerInfo);
            var sut = new CustomerController(customer);
            var res = sut.GetCustomerDetails(1);

            var okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);

            var returnedCustomers = okresult.Value as Customer;
            returnedCustomers.Should().NotBeNull();
            returnedCustomers.Should().BeEquivalentTo(customerInfo);
        }

        [Fact()]
        public void AddCustomerTest()
        {
            var customerInfo = new Customer
            {
                Id = 1,
                Firstname = "Test",
                Middlename = "Test",
                Lastname = "Test",
                Age = 1,
                Sex = "M"
            };

            var sut = new CustomerController(customer);
            var res = sut.AddCustomer(customerInfo);
            
            res.Should().NotBeNull();
        }

        [Fact()]
        public void EditCustomerTest()
        {
            var customerInfo = new Customer
            {
                Id = 1,
                Firstname = "Test",
                Middlename = "Test",
                Lastname = "Test",
                Age = 1,
                Sex = "M"
            };

            A.CallTo(() => this.customer.EditCustomer(A<Customer>.Ignored)).Returns(true);
            var sut = new CustomerController(customer);
            var res = sut.EditCustomer(customerInfo);

            var okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);
        }

        [Fact()]
        public void DeleteCustomerTest()
        {
            A.CallTo(() => this.customer.DeleteCustomer(A<int>.Ignored)).Returns(true);
            var sut = new CustomerController(customer);
            var res = sut.DeleteCustomer(1);

            var okresult = res as OkObjectResult;

            okresult.Should().NotBeNull();
            okresult!.StatusCode.Should().Be(200);
        }
    }
}