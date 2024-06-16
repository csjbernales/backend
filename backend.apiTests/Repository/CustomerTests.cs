using backend.api.Data.Generated;

namespace backend.api.Models.Generated.Tests
{
    public class CustomerTests
    {
        private DbContextOptions<FullstackDBContext> dbContextOptions = new();

        [Fact()]
        public void A_GetAllCustomersTest()
        {
            //Arrange
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

            Customer sut = new(fullstackDBContext);

            // Act
            IList<Customer> allCustomers = sut.GetAllCustomers();

            // Assert
            allCustomers.Should().HaveCount(1);
        }

        [Fact()]
        public void B_GetCustomerDetailsTest()
        {
            //Arrange
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

            Customer sut = new(fullstackDBContext);

            // Act
            Customer? allCustomers = sut.GetCustomerDetails(2);

            // Assert
            allCustomers.Should().BeSameAs(cust);
        }

        [Fact()]
        public void C_AddCustomerTest()
        {
            //Arrange
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
            fullstackDBContext.Customers.Add(cust);
            fullstackDBContext.SaveChanges();

            Customer sut = new(fullstackDBContext);

            // Act
            sut.AddCustomer(cust);
            Customer? allCustomers = sut.GetCustomerDetails(3);

            // Assert
            allCustomers.Should().BeSameAs(cust);
        }

        [Fact()]
        public void D_EditCustomerTest()
        {
            //Arrange
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
            fullstackDBContext.Add(cust);
            fullstackDBContext.SaveChanges();

            Customer? updatedCust = fullstackDBContext.Customers.Where(x => x.Id == cust.Id).FirstOrDefault();

            updatedCust!.Age = 26;

            Customer sut = new(fullstackDBContext);

            // Act
            bool allCustomers = sut.EditCustomer(updatedCust);

            // Assert
            allCustomers.Should().BeTrue();
        }

        [Fact()]
        public void E_DeleteCustomerTest()
        {
            //Arrange
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
            fullstackDBContext.Customers.Add(cust);
            fullstackDBContext.SaveChanges();

            Customer sut = new(fullstackDBContext);

            // Act
            bool del = sut.DeleteCustomer(5);

            Customer? allCustomers = sut.GetCustomerDetails(5);
            // Assert
            allCustomers.Should().Be(null);
            del.Should().BeTrue();
        }
    }
}