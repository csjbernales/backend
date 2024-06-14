using backend.api.Data.Generated;
using Xunit;

namespace backend.api.Models.Generated.Tests
{
    public class CustomerTests
    {
        private readonly DbContextOptions<FullstackDBContext> dbContextOptions;

        public CustomerTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

        }

        [Fact()]
        public void GetAllCustomersTest()
        {
            //Arrange
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

            Customer sut = new(fullstackDBContext);

            // Act
            IList<Customer> allCustomers = sut.GetAllCustomers();

            // Assert
            allCustomers.Should().HaveCount(1);
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