using backend.api.Data.Generated;
using backend.api.Service;
using Xunit.Priority;
using PriorityAttribute = Xunit.Priority.PriorityAttribute;

namespace backend.api.Models.Generated.Tests
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
                    Id = 1,
                    Firstname = "Verna",
                    Middlename = "Dorias",
                    Lastname = "Bernales",
                    Age = 25,
                    Sex = "F",
                    IsEmployed = true
                });
            fullstackDBContext.SaveChanges();

            CustomersService sut = new(fullstackDBContext);

            IList<Customer> allCustomers = sut.GetAllCustomers();

            allCustomers.Should().HaveCount(1);
        }

        [Fact()]
        [Priority(2)]
        public void B_GetCustomerDetailsTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Customer cust = new()
            {
                Id = 2,
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

            Customer? allCustomers = sut.GetCustomerDetails(2);

            allCustomers.Should().BeSameAs(cust);
        }

        [Fact()]
        [Priority(3)]
        public async Task C_AddCustomerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Customer cust = new()
            {
                Id = 3,
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
            Customer? allCustomers = sut.GetCustomerDetails(3);

            allCustomers.Should().BeSameAs(cust);
        }

        [Fact()]
        [Priority(4)]
        public async Task D_EditCustomerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Customer cust = new()
            {
                Id = 4,
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
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            Customer cust = new()
            {
                Id = 5,
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

            bool del = await sut.DeleteCustomer(5);

            Customer? allCustomers = sut.GetCustomerDetails(5);
            allCustomers.Should().Be(null);
            del.Should().BeTrue();
        }
    }
}