using backend.api.Data.Generated;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Xunit;
using Xunit.Priority;
using PriorityAttribute = Xunit.Priority.PriorityAttribute;

namespace backend.api.Models.Generated.Tests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class ProductTests
    {
        private DbContextOptions<FullstackDBContext> dbContextOptions = new();

        [Fact, Priority(0)]
        public void A_GetAllProductsTest()
        {
            //Arrange
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            fullstackDBContext.Products.Add(
                new Product
                {
                    Id = 1,
                    Name = "Item name",
                    Category = "Food",
                    Quantity = 10

                });
            fullstackDBContext.SaveChanges();

            Product sut = new(fullstackDBContext);

            // Act
            IList<Product> allProducts = sut.GetAllProducts();

            // Assert
            allProducts.Should().HaveCount(1);
        }

        [Fact()]
        public void B_GetProductDetailsTest()
        {
            //Arrange
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            var cust = new Product
            {
                Id = 2,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };
            fullstackDBContext.Add(cust);
            fullstackDBContext.SaveChanges();

            Product sut = new(fullstackDBContext);

            // Act
            Product? allProducts = sut.GetProductDetails(2);

            // Assert
            allProducts.Should().BeSameAs(cust);
        }

        [Fact()]
        public void C_AddProductTest()
        {
            //Arrange
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            var cust = new Product
            {
                Id = 3,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };
            fullstackDBContext.Products.Add(cust);
            fullstackDBContext.SaveChanges();

            Product sut = new(fullstackDBContext);

            // Act
            sut.AddProduct(cust);
            Product? allProducts = sut.GetProductDetails(3);

            // Assert
            allProducts.Should().BeSameAs(cust);
        }

        [Fact()]
        public void D_EditProductTest()
        {
            //Arrange
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            var cust = new Product
            {
                Id = 4,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };
            fullstackDBContext.Add(cust);
            fullstackDBContext.SaveChanges();

            var updatedCust = fullstackDBContext.Products.Where(x => x.Id == cust.Id).FirstOrDefault();

            updatedCust.Quantity = 26;

            Product sut = new(fullstackDBContext);

            // Act
            var allProducts = sut.EditProduct(updatedCust);

            // Assert
            allProducts.Should().BeTrue();
        }

        [Fact()]
        public void E_DeleteProductTest()
        {
            //Arrange
            dbContextOptions = new DbContextOptionsBuilder<FullstackDBContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            using FullstackDBContext fullstackDBContext = new(dbContextOptions);
            var cust = new Product
            {
                Id = 5,
                Name = "Item name",
                Category = "Food",
                Quantity = 10
            };
            fullstackDBContext.Products.Add(cust);
            fullstackDBContext.SaveChanges();

            Product sut = new(fullstackDBContext);

            // Act
            var del = sut.DeleteProduct(5);

            var allProducts = sut.GetProductDetails(5);
            // Assert
            allProducts.Should().Be(null);
            del.Should().BeTrue();
        }
    }
}