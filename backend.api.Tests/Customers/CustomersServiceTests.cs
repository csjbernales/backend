using backend.api.Customers;
using backend.data.Data.Generated;
using backend.data.Models.Dto;
using backend.data.Models.Generated;
using Xunit.Priority;
using PriorityAttribute = Xunit.Priority.PriorityAttribute;

namespace backend.api.Tests.Customers
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class CustomersServiceTests
    {
        private DbContextOptions<FullstackDBContext> dbContextOptions = new();

        [Fact()]
        [Priority(1)]
        public void A_GetAllCustomersTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            fullstackDBContext.Customers.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Verna",
                    Middlename = "Dorias",
                    Lastname = "Bernales",
                    Age = 25,
                    Sex = "F",
                    IsEmployed = true
                });
            fullstackDBContext.SaveChanges();

            CustomersService sut = new(fullstackDBContext);

            IReadOnlyList<CustomersDto> allCustomers = sut.GetAllCustomers();

            allCustomers.Should().HaveCount(1);
        }

        [Fact()]
        [Priority(2)]
        public void B_GetCustomerDetailsTest()
        {
            Guid guid = Guid.NewGuid();
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Customer cust = new()
            {
                Id = guid,
                Firstname = "Verna",
                Middlename = "Dorias",
                Lastname = "Bernales",
                Age = 25,
                Sex = "F",
                IsEmployed = true
            };
            fullstackDBContext.Add(cust);
            fullstackDBContext.SaveChanges();

            CustomersService sut = new(fullstackDBContext);

            CustomersDto allCustomers = sut.GetCustomerDetails(guid)!;

            Assert.NotNull(allCustomers);
        }

        [Fact()]
        [Priority(3)]
        public async Task C_AddCustomerTest()
        {
            Guid guid = Guid.NewGuid();
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Customer cust = new()
            {
                Id = guid,
                Firstname = "Verna",
                Middlename = "Dorias",
                Lastname = "Bernales",
                Age = 25,
                Sex = "F",
                IsEmployed = true
            };
            await fullstackDBContext.Customers.AddAsync(cust);
            await fullstackDBContext.SaveChangesAsync();

            CustomersService sut = new(fullstackDBContext);

            await sut.AddCustomer(cust);
            CustomersDto allCustomers = sut.GetCustomerDetails(guid)!;

            Assert.NotNull(allCustomers);
        }

        [Fact()]
        [Priority(4)]
        public async Task D_EditCustomerTest()
        {
            Guid guid = Guid.NewGuid();
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Customer cust = new()
            {
                Id = guid,
                Firstname = "Verna",
                Middlename = "Dorias",
                Lastname = "Bernales",
                Age = 25,
                Sex = "F",
                IsEmployed = true
            };
            await fullstackDBContext.AddAsync(cust);
            await fullstackDBContext.SaveChangesAsync();

            Customer? updatedCust = fullstackDBContext.Customers.Where(x => x.Id == cust.Id).FirstOrDefaultAsync().Result;

            updatedCust!.Age = 26;

            CustomersService sut = new(fullstackDBContext);

            bool allCustomers = await sut.EditCustomer(updatedCust);

            allCustomers.Should().BeTrue();
        }

        [Fact()]
        [Priority(5)]
        public async Task E_DeleteCustomerTest()
        {
            Guid guid = Guid.NewGuid();
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Customer cust = new()
            {
                Id = guid,
                Firstname = "Verna",
                Middlename = "Dorias",
                Lastname = "Bernales",
                Age = 25,
                Sex = "F",
                IsEmployed = true
            };
            await fullstackDBContext.Customers.AddAsync(cust);
            await fullstackDBContext.SaveChangesAsync();

            CustomersService sut = new(fullstackDBContext);

            bool del = await sut.DeleteCustomer(guid);

            CustomersDto allCustomers = sut.GetCustomerDetails(guid)!;
            allCustomers.Should().Be(null);
            del.Should().BeTrue();
        }
    }
}